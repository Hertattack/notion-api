import QueryApiResult from "./QueryApiResult";

export default class FailedQueryAnalysisResult extends QueryApiResult {
    public message: string;
    public data: any;

    constructor(message: string, data: any, stack: string) {
        super();
        this.message = message;
        this.data = data;
    }
}