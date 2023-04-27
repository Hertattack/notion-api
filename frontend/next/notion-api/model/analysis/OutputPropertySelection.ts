import QueryDatabaseReference from "@/notion-api/model/analysis/QueryDatabaseReference";

export default interface OutputPropertySelection {
    databaseReference : QueryDatabaseReference;
    propertyNames : string[]
}