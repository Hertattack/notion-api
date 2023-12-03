import FieldValueSet from "./FieldValueSet.ts";

export default interface Row {
    fieldValueSets : {
        [databaseAlias: string]: FieldValueSet
    }
}