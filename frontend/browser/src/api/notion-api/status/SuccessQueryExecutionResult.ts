import QueryApiResult from "./QueryApiResult";
import QueryResult from "../model/QueryResult.ts";

export default class SuccessQueryExecutionResult extends QueryApiResult {
    public result: QueryResult;

    constructor(result: QueryResult) {
        super();
        this.result = result;
    }
}