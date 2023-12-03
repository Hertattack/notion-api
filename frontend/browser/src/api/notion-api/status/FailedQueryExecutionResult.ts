import QueryApiResult from "./QueryApiResult";

export default class FailedQueryExecutionResult extends QueryApiResult {
    public message: string;
    public data: unknown;
    public stack: string | undefined;

    constructor(message: string, data: unknown, stack: string | undefined) {
        super();
        this.message = message;
        this.data = data;
        this.stack = stack;
    }
}