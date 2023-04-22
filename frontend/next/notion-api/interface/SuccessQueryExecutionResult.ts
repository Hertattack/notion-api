import QueryExecutionResult from "./QueryExecutionResult";
import {QueryResult} from "../query";

export default class SuccessQueryExecutionResult extends QueryExecutionResult {
    public result: QueryResult;

    constructor(result: QueryResult) {
        super();
        this.result = result;
    }
}