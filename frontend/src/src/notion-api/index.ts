import axios from "axios";
import configuration from './configuration';
import QueryApi from "./query";

const axiosClient = axios.create({
    baseURL: configuration.baseUri,
    timeout: 100000
});

export default {
    query: new QueryApi(axiosClient)
}