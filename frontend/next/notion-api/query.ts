import axios, {AxiosError, AxiosInstance} from "axios";
import SuccessQueryExecutionResult from "./status/SuccessQueryExecutionResult";
import FailedQueryExecutionResult from "./status/FailedQueryExecutionResult";
import FailedQueryAnalysisResult from "@/notion-api/status/FailedQueryAnalysisResult";
import {QueryPlan} from "@/notion-api/model/analysis/QueryPlan";
import QueryResult from "@/notion-api/model/QueryResult";

export default class QueryApi {
    private client: AxiosInstance;
    constructor(axiosInstance: AxiosInstance) {
        this.client = axiosInstance;
    }

    public async AnalyzeQuery(queryText: string) {
        return await this.client.post("QueryAnalysis/", { QueryText: queryText})
            .then(
                response => {
                    if(response.status < 200 || response.status >= 300)
                        return new FailedQueryAnalysisResult(`Failed to analyze query. Status code: ${response.status}.`, response.data, "");

                    return new SuccessQueryExecutionResult(response.data as QueryPlan);
                },
                error => {
                    if(axios.isAxiosError(error)) {
                        const axiosError = <AxiosError>error;

                        return new FailedQueryAnalysisResult('Some error occurred while analyzing the query.', axiosError.response !== undefined ? axiosError.response.data : "", axiosError.stack ?? "");
                    }
                    return new FailedQueryAnalysisResult("Unexpected error occurred", error.message, error.stack)
                }
            )
    }

    public async ExecuteQuery(queryText: string){
        return await this.client.get("Query/",{ params: { query: queryText} })
            .then(
                response => {
                    if(response.status < 200 || response.status >= 300)
                        return new FailedQueryExecutionResult(`Failed to execute query. Status code: ${response.status}.`, response.data, "");

                    return new SuccessQueryExecutionResult(response.data as QueryResult);
                },
                error => {
                    if(axios.isAxiosError(error)) {
                        const axiosError = <AxiosError>error;

                        return new FailedQueryExecutionResult('Some error occurred while executing the query.', axiosError.response !== undefined ? axiosError.response.data : "", axiosError.stack ?? "");
                    }
                    return new FailedQueryExecutionResult("Unexpected error occurred", error.message, error.stack);
                });
    }
}