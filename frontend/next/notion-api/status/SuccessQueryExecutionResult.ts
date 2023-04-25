import QueryExecutionResult from "./QueryExecutionResult";
import {QueryResult} from "../query";
import {QueryPlan} from "@/notion-api/interface/QueryPlan";

export default class SuccessQueryExecutionResult extends QueryExecutionResult {
    public result: QueryPlan;

    constructor(result: QueryPlan) {
        super();
        this.result = result;
    }
}