import {AxiosInstance} from "axios";
import RequestError from "./RequestError";
import {Metamodel} from "./interface/Metamodel";

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
}