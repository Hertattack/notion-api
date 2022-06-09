using NotionApi.Rest.Response.Page.Properties;
using NotionApi.Rest.Response.Page.Properties.Relation;
using NotionGraphDatabase.Util;

namespace NotionGraphDatabase.Storage.Mappers;

public static class PropertyValueMapper
{
    public static object? Map(NotionPropertyValue notionObjectProperty)
    {
        return notionObjectProperty switch
        {
            CheckboxPropertyValue checkbox => checkbox.Checkbox.ValueOrDefault(),
            CreatedByPropertyValue createdByPropertyValue => createdByPropertyValue.CreatedBy.ValueOrDefault()?.Name ??
                                                             "unknown",
            CreateTimePropertyValue createTimePropertyValue => createTimePropertyValue.CreatedTime.ValueOrDefault(),
            DatePropertyValue date => date.Date.ValueOrDefault(),
            EmailPropertyValue email => email.Email.ValueOrDefault(),
            FilePropertyValue file => file.Files.ValueOrElse(new List<FileReference>()).Select(fr => fr.Name).ToList(),
            FormulaPropertyValue formula => formula.Formula.ValueOrDefault()?.Type,
            LastEditedByPropertyValue lastEditedByPropertyValue => lastEditedByPropertyValue.LastEditedBy
                .ValueOrDefault()?.Name ?? "unknown",
            LastEditedPropertyValue lastEditedPropertyValue => lastEditedPropertyValue.LastEditedTime.ValueOrDefault(),
            MultiSelectPropertyValue multiSelectPropertyValue => multiSelectPropertyValue.SelectedOptions.ValueOrElse(
                new List<SelectOptionValue>()),
            NumberPropertyValue number => number.Number.ValueOrDefault(),
            PeoplePropertyValue peoplePropertyValue => peoplePropertyValue.People.ValueOrDefault(),
            PhoneNumberPropertyValue phoneNumberPropertyValue => phoneNumberPropertyValue.PhoneNumber.ValueOrDefault(),
            OneToManyRelationPropertyValue oneToManyRelationPropertyValue => oneToManyRelationPropertyValue.Relations
                .Select(r => r.Id).ToList(),
            RichTextPropertyValue richTextPropertyValue => richTextPropertyValue.ToString(),
            RollupPropertyValue rollupPropertyValue => rollupPropertyValue.Rollup.ValueOrDefault()?.Type ?? "unknown",
            SelectPropertyValue selectPropertyValue => selectPropertyValue.SelectedOption.ValueOrDefault(),
            TitlePropertyValue titlePropertyValue => titlePropertyValue.ToString(),
            UrlPropertyValue urlPropertyValue => urlPropertyValue.Url.ValueOrDefault(),
            _ => throw new ArgumentOutOfRangeException(nameof(notionObjectProperty), notionObjectProperty, null)
        };
    }
}