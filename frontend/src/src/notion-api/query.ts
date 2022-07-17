import {AxiosInstance} from "axios";
import FieldIdentifier from "./interface/FieldIdentifier";
import RequestError from "./RequestError";

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
                    if(response.status >= 200 && response.status < 300)
                        return response.data as QueryResult;

                    throw new RequestError(`Failed to execute query. Status code: ${response.status}.`, response.data);
                });
    }
}