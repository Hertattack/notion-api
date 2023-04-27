import QueryDatabaseReference from "@/notion-api/model/analysis/QueryDatabaseReference";
import OutputPropertySelection from "@/notion-api/model/analysis/OutputPropertySelection";
import QuerySelectStep from "@/notion-api/model/analysis/QuerySelectStep";

export default interface QuerySpecification {
    databaseReferences : QueryDatabaseReference[],
    allPropertiesSelected: QueryDatabaseReference[],
    propertiesSelected: OutputPropertySelection[],
    selectSteps: QuerySelectStep[]
}