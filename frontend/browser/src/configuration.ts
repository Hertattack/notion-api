import {NotionStoreConfiguration} from "./store/Notion/NotionStore.ts";

const configuration : { notionApi: NotionStoreConfiguration } = {
    notionApi : {
        baseUri: "http://localhost:5136/",
        timeout: 1000000
    }
};

export default configuration;