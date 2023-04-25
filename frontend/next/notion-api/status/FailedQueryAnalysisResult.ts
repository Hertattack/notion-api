import QueryExecutionResult from "./QueryExecutionResult";

export default class FailedQueryAnalysisResult extends QueryExecutionResult {
    public message: string;
    public data: any;

    constructor(message: string, data: any, stack: string) {
        super();
        this.message = message;
        this.data = data;
    }
}