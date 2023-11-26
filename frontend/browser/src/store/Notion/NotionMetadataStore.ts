import {NotionClient} from "../../api/notion-api/NotionClient.ts";


export class NotionMetadataStore {
    private client: NotionClient;

    constructor(client: NotionClient) {
        this.client = client;
    }
}