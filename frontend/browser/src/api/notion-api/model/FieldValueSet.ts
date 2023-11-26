export default interface FieldValueSet {
    alias: string,
    values: {
        [propertyName: string]: any
    }
}