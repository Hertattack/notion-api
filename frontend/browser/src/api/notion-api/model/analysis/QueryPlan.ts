import QuerySpecification from "@/notion-api/model/analysis/QuerySpecification";
import QueryPlanStep from "@/notion-api/model/analysis/QueryPlanStep";

export interface QueryPlan {
    querySpecification : QuerySpecification,
    planSteps : QueryPlanStep[]
}