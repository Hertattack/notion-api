import axios, {AxiosInstance} from "axios";
import MetamodelApi from "./MetamodelApi.ts";
import QueryApi from "./QueryApi.ts";

export class NotionClient {
    private axiosClient: AxiosInstance;

    private metamodelApi: MetamodelApi;

    get Metamodel() : MetamodelApi {
        return this.metamodelApi;
    }

    private queryApi: QueryApi;

    get Query() : QueryApi {
        return this.queryApi;
    }

    constructor(baseUri: string, timeout: number) {
        this.axiosClient = axios.create({
            baseURL: baseUri,
            timeout: timeout
        });

        this.metamodelApi = new MetamodelApi(this.axiosClient);
        this.queryApi = new QueryApi(this.axiosClient);
    }
}

