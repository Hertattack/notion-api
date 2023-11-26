import {NotionStore, NotionStoreConfiguration} from "./Notion/NotionStore.ts";

class RootStore {
    notionStore: NotionStore;

    constructor(notionStoreConfiguration: NotionStoreConfiguration) {
        this.notionStore = new NotionStore(notionStoreConfiguration);
    }
}

export default RootStore;