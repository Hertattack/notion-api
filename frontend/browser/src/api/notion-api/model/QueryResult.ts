import FieldIdentifier from "./FieldIdentifier.ts";
import Row from "./Row.ts";

export default interface QueryResult {
    propertyNames: FieldIdentifier[],
    rows : Row[]
}