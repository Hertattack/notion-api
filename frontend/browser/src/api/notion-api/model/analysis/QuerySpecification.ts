import QueryDatabaseReference from "./QueryDatabaseReference.ts";
import OutputPropertySelection from "./OutputPropertySelection.ts";
import QuerySelectStep from "./QuerySelectStep.ts";

export default interface QuerySpecification {
    databaseReferences : QueryDatabaseReference[],
    allPropertiesSelected: QueryDatabaseReference[],
    propertiesSelected: OutputPropertySelection[],
    selectSteps: QuerySelectStep[]
}