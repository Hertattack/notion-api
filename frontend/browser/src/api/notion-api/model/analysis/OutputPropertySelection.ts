import QueryDatabaseReference from "./QueryDatabaseReference.ts";

export default interface OutputPropertySelection {
    databaseReference : QueryDatabaseReference;
    propertyNames : string[]
}