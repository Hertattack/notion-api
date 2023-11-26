import QueryApiResult from "./QueryApiResult";
import QueryResult from "@/notion-api/model/QueryResult";
export default class SuccessQueryExecutionResult extends QueryApiResult {
    public result: QueryResult;

    constructor(result: QueryResult) {
        super();
        this.result = result;
    }
}