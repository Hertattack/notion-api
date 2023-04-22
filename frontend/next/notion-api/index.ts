import axios from "axios";
import configuration from './configuration';
import QueryApi from "./query";
import MetamodelApi from "./metamodel";

const axiosClient = axios.create({
    baseURL: configuration.baseUri,
    timeout: 100000
});

const notionApi = {
    query: new QueryApi(axiosClient),
    metamodel: new MetamodelApi(axiosClient)
}

export default notionApi;