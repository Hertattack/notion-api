import axios, {AxiosError, AxiosInstance} from "axios";
import FieldIdentifier from "./interface/FieldIdentifier";
import SuccessQueryExecutionResult from "./interface/SuccessQueryExecutionResult";
import FailedQueryExecutionResult from "./interface/FailedQueryExecutionResult";

export interface FieldValueSet {
    alias: string,
    values: {
        [propertyName: string]: any
    }
}

export interface Row {
    fieldValueSets : {
        [databaseAlias: string]: FieldValueSet
    }
}

export interface QueryResult {
    propertyNames: FieldIdentifier[],
    rows : Row[]
}

export default class QueryApi {
    private client: AxiosInstance;
    constructor(axiosInstance: AxiosInstance) {
        this.client = axiosInstance;
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
                    return new FailedQueryExecutionResult("Unexpected error occurred", error.message, error.stack)
                });
    }
}