import QueryDatabaseReference from "./QueryDatabaseReference.ts";
import QueryFilterExpression from "./QueryFilterExpression.ts";

export default interface QuerySelectStep {
    databaseReference : QueryDatabaseReference,
    alias: string,
    filters : QueryFilterExpression[],
}