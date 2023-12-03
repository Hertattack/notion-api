import QuerySpecification from "./QuerySpecification.ts";
import QueryPlanStep from "./QueryPlanStep.ts";

export interface QueryPlan {
    querySpecification : QuerySpecification,
    planSteps : QueryPlanStep[]
}