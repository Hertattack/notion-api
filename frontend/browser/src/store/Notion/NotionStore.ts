import {NotionMetadataStore} from "./NotionMetadataStore.ts";
import {NotionClient} from "../../api/notion-api/NotionClient.ts";

export interface NotionStoreConfiguration {
    baseUri: string;
    timeout: number;
}

export class NotionStore {
    metadataStore: NotionMetadataStore;
    private client: NotionClient;

    constructor(configuration: NotionStoreConfiguration) {
        this.client = new NotionClient(configuration.baseUri, configuration.timeout);
        this.metadataStore = new NotionMetadataStore(this.client);
    }

}