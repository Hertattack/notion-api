using Microsoft.Extensions.Logging;
using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.Query;
using NotionGraphDatabase.Query.Expression;
using NotionGraphDatabase.Query.Filter;
using NotionGraphDatabase.Query.Path;

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
            {
                ValidateFilterOperator(validationResult, filter);

                if (filter.Expression is PropertyIdentifier propertyValueCompareExpression)
                    ValidatePropertyValueComparisonFilter(validationResult, propertyValueCompareExpression,
                        stepContext);
            }
        }
    }

    private static void ValidateFilterOperator(ValidationResult validationResult, FilterExpression filter)
    {
        var comparisonOperator = filter.Operator;
        switch (filter.Expression)
        {
            case StringExpression:
            {
                if (!(comparisonOperator.Type
                          is ComparisonType.EQUALS
                          or ComparisonType.CONTAINS
                      || (!comparisonOperator.IsNegated
                          && comparisonOperator.Type
                              is ComparisonType.ENDS_WITH
                              or ComparisonType.STARTS_WITH)))
                    validationResult.AddError(new ValidationError(ValidationErrorCodes.OPERATOR_NOT_SUPPORTED,
                        $"Operator {comparisonOperator} is not supported for a string value comparison"));
                break;
            }
            case IntegerExpression:
            {
                if (!(comparisonOperator.Type is ComparisonType.EQUALS
                      || (comparisonOperator.Type
                              is ComparisonType.GREATER_THAN
                              or ComparisonType.LESS_THAN
                              or ComparisonType.GREATER_OR_EQUAL
                              or ComparisonType.LESS_OR_EQUAL
                          && !comparisonOperator.IsNegated)))
                    validationResult.AddError(new ValidationError(ValidationErrorCodes.OPERATOR_NOT_SUPPORTED,
                        $"Operator {comparisonOperator} is not supported for an integer value comparison"));

                break;
            }
            case PropertyIdentifier:
            {
                if (comparisonOperator.Type
                    is not (ComparisonType.EQUALS
                    or ComparisonType.CONTAINS
                    or ComparisonType.ENDS_WITH
                    or ComparisonType.STARTS_WITH))
                    validationResult.AddError(new ValidationError(ValidationErrorCodes.OPERATOR_NOT_SUPPORTED,
                        $"Operator {comparisonOperator} is not supported for an property value comparison"));
                break;
            }
            default:
                validationResult.AddError(new ValidationError(ValidationErrorCodes.EXPRESSION_NOT_SUPPORTED,
                    $"Expression type {filter.Expression.GetType().FullName} is not supported"));
                break;
        }
    }

    private static void ValidatePropertyValueComparisonFilter(ValidationResult validationResult,
        PropertyIdentifier propertyIdentifier, NodeSelectStepContext stepContext)
    {
        var aliasFound = false;
        var alias = propertyIdentifier.Alias;
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
            $"The alias: '{alias}' using the filter: {{{propertyIdentifier}}} at last position in the following path section: {stepContext.ToPathString()}."
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