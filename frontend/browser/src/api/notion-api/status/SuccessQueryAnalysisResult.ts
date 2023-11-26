import QueryApiResult from "./QueryApiResult";
import {QueryPlan} from "@/notion-api/model/analysis/QueryPlan";
export default class SuccessQueryAnalysisResult extends QueryApiResult {
    public result: QueryPlan;

    constructor(result: QueryPlan) {
        super();
        this.result = result;
    }
}