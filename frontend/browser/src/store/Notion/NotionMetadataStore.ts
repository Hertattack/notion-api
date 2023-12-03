import {NotionClient} from "../../api/notion-api/NotionClient.ts";
import Metamodel from "../../api/notion-api/model/metadata/Metamodel.ts";
import {action, makeObservable, observable} from "mobx";

export class NotionMetadataStore {
    private client: NotionClient;

    @observable metamodel : Metamodel;
    @observable status: string;

    constructor(client: NotionClient) {
        makeObservable(this);
        this.client = client;
        this.metamodel = {
            databases: [],
            edges: []
        };
        this.status = "";
    }

    @action
    initialize = ()=> {
        this.client.Metamodel.Get()
            .then( m=>{
            this.metamodel.databases = m.databases;
            this.metamodel.edges = m.edges; })
            .catch( e => {
                this.status = e.message;
            })
    }
}