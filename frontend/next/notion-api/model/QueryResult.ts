import FieldIdentifier from "@/notion-api/model/FieldIdentifier";
import Row from "@/notion-api/model/Row";

export default interface QueryResult {
    propertyNames: FieldIdentifier[],
    rows : Row[]
}