import QueryDatabaseReference from "@/notion-api/model/analysis/QueryDatabaseReference";
import QueryFilterExpression from "@/notion-api/model/analysis/QueryFilterExpression";

export default interface QuerySelectStep {
    databaseReference : QueryDatabaseReference,
    alias: string,
    filters : QueryFilterExpression[],
}