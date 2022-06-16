using Microsoft.Extensions.Logging;
using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.QueryEngine.Query;
using NotionGraphDatabase.QueryEngine.Query.Expression;
using NotionGraphDatabase.QueryEngine.Query.Path;

namespace NotionGraphDatabase.QueryEngine.Validation;

internal class QueryValidator : IQueryValidator
{
    private readonly ILogger<QueryValidator> _logger;

    public QueryValidator(ILogger<QueryValidator> logger)
    {
        _logger = logger;
    }

    private ValidationResult Validate(IQuery query)
    {
        _logger.LogDebug("Validating query semantics");
        var validationResult = new ValidationResult();

        ValidateAliases(query, validationResult);
        ValidateFilters(query, validationResult);

        _logger.LogDebug("Finished semantic query validation");
        return validationResult;
    }

    private static void ValidateFilters(IQuery query, ValidationResult validationResult)
    {
        foreach (var stepContext in query.SelectSteps.Cast<NodeSelectStepContext>())
        {
            var step = (NodeSelectStep) stepContext.Step;
            foreach (var filter in step.Filter)
                if (filter.Expression is PropertyValueCompareExpression propertyValueCompareExpression)
                    ValidatePropertyValueComparisonFilter(validationResult, propertyValueCompareExpression,
                        stepContext);
        }
    }

    private static void ValidatePropertyValueComparisonFilter(ValidationResult validationResult,
        PropertyValueCompareExpression propertyValueCompareExpression, NodeSelectStepContext stepContext)
    {
        var aliasFound = false;
        var alias = propertyValueCompareExpression.RightAlias;
        var previousStep = stepContext.PreviousStepContext;
        while (previousStep is not null && !aliasFound)
        {
            if (previousStep.Step.AssociatedNode.Alias == alias)
                aliasFound = true;

            previousStep = previousStep.PreviousStepContext;
        }

        if (aliasFound) return;

        var validationError = new ValidationError(
            ValidationErrorCodes.ALIAS_NOT_DEFINED,
            $"The alias: '{alias}' using the filter: {{{propertyValueCompareExpression}}} at last position in the following path section: {stepContext.ToPathString()}."
        );
        validationResult.AddError(validationError);
    }

    private static void ValidateAliases(IQuery query, ValidationResult validationResult)
    {
        var duplicateAliases = query.NodeReferences
            .GroupBy(r => r.Alias)
            .Where(g => g.Count() > 1)
            .ToList();

        if (duplicateAliases.Count > 0)
        {
            var duplicates = string.Join("\n",
                duplicateAliases.Select(d =>
                    $"Alias: '{d.Key}' used for nodes: {string.Join(", ", d.GetEnumerator())}."));

            var validationError = new ValidationError(
                ValidationErrorCodes.DUPLICATE_ALIASES,
                $"Same alias has been used in different select steps, this is not supported. {duplicates}");

            validationResult.AddError(validationError);
        }
    }

    public ValidationResult Validate(IQuery query, Metamodel metamodelStoreMetamodel)
    {
        _logger.LogDebug("Validating query against metamodel");
        var validationResult = Validate(query);
        _logger.LogDebug("Finished query validation");
        return validationResult;
    }
}