import {AxiosInstance} from "axios";
import Metamodel from "./model/metadata/Metamodel.ts";
import RequestError from "./model/RequestError.ts";
import {DatabaseDefinition} from "./model/DatabaseDefinition.ts";

export default class MetamodelApi {
    private client: AxiosInstance;

    constructor(axiosInstance: AxiosInstance) {
        this.client = axiosInstance;
    }

    public async Get(){
        return await this.client.get("Metamodel")
            .then(
                response => {
                    if(response.status >= 200 && response.status < 300)
                        return response.data as Metamodel;

                    throw new RequestError(`Failed to execute query. Status code: ${response.status}.`, response.data);
                });
    }

    public async GetDatabaseDefinition(alias: string ){
        return await this.client
            .get("Metamodel/databaseDefinition", {params : { alias }})
            .then(
                response => {
                    if(response.status >= 200 && response.status < 300)
                        return response.data as DatabaseDefinition;

                    throw new RequestError(`Failed to execute query. Status code: ${response.status}.`, response.data);
                });
    }
}