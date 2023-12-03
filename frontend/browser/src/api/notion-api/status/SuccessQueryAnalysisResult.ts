import QueryApiResult from "./QueryApiResult";
import {QueryPlan} from "../model/analysis/QueryPlan.ts";

export default class SuccessQueryAnalysisResult extends QueryApiResult {
    public result: QueryPlan;

    constructor(result: QueryPlan) {
        super();
        this.result = result;
    }
}