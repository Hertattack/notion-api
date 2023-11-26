import FieldValueSet from "@/notion-api/model/FieldValueSet";

export default interface Row {
    fieldValueSets : {
        [databaseAlias: string]: FieldValueSet
    }
}