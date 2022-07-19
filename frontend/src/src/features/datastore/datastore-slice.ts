import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {QueryResult} from "../../notion-api/query";
import {DatabaseReference, Metamodel} from "../../notion-api/interface/Metamodel";
import {DatabaseDefinitions} from "../metamodel/metamodel-slice";
import FieldIdentifier from "../../notion-api/interface/FieldIdentifier";
import {DatabaseDefinition} from "../../notion-api/interface/DatabaseDefinition";

interface Category {
    id: string,
    name: string
}

export interface Node {
    id: string,
    label: string,
    databaseAlias: string,
    category: number
}

interface Edge {
    source: string,
    target: string
}

interface DataStore {
    nodes: {[nodeId: string]: Node},
    categories: Category[],
    edges: Edge[]
}

export interface DataStoreState {
    data: DataStore
}

const initialState: DataStoreState = {
    data: {
        nodes: {},
        categories: [],
        edges: []
    }
}

export interface DataStoreUpdatePayload {queryResult: QueryResult, metamodel: Metamodel, databaseDefinitions: DatabaseDefinitions}

const dataStoreSlice = createSlice({
   name: 'datastore',
   initialState,
   reducers: {
    updateDataStore: (state, action: PayloadAction<DataStoreUpdatePayload>)=>{
        let {nodes,categories, edges} = state.data;
        const {queryResult, metamodel, databaseDefinitions} = action.payload;

        const categoryMapping = updateCategories(metamodel.databases, categories);
        const databaseAliases = mapPropertyNamesToDatabases(queryResult.propertyNames);
        const databaseDefinitionsByAlias: { [databaseAlias: string]: DatabaseDefinition } = mapToDefinitionsByAlias(metamodel, databaseDefinitions);
        const relationsPerDatabaseAlias = getRelationFieldsPerAlias(metamodel);
        const titleFields: {[databaseAlias: string]: string|undefined} = {};
        databaseAliases.forEach(a=> titleFields[a] = databaseDefinitionsByAlias[a].properties.find(p=>p.type==='title')?.name);

        let nodeIds: string[] = [];
        let updatedNodes: {[nodeId: string]: Node} = {}
        let potentialRelations: Edge[]  = [];
        queryResult.rows.forEach( r => {
            databaseAliases.forEach( d => {
                let valueSet = r.fieldValueSets[d].values;
                let pageId = valueSet.Id;

                if(updatedNodes[pageId])
                    return;

                let titleFieldName = titleFields[d];
                updatedNodes[pageId] = {
                    id: pageId,
                    label: titleFieldName ? valueSet[titleFieldName] : `No label`,
                    databaseAlias: d,
                    category: categoryMapping[d]
                };

                nodeIds.push(pageId);

                relationsPerDatabaseAlias[d].forEach( r => {
                    let fieldValue = valueSet[r.id] as string[];
                    if(fieldValue){
                        fieldValue.forEach( v => potentialRelations.push({source: pageId, target: v}));
                    }
                });
            });
        });

        nodeIds.forEach(id => nodes[id] = updatedNodes[id]);
        potentialRelations.filter((r, i, self) =>
                !self.some( (v,vi) => v.source === r.source && v.target === r.target && i > vi) )
            .forEach( r =>{
                if(!nodes[r.source] || !nodes[r.target])
                    return;

                if(!edges.some( e=>e.target === r.target && r.source === e.source))
                    edges.push(r);
            });
    }
   }
});

export const {updateDataStore} = dataStoreSlice.actions;
export default dataStoreSlice.reducer;

function mapPropertyNamesToDatabases(propertyNames: FieldIdentifier[]) : string[] {
    return propertyNames
        .map(p=>p.alias ?? "")
        .filter(v=>v!=="")
        .filter((v,i,self)=>self.indexOf(v)===i);
}

function updateCategories(databases: DatabaseReference[], categories: Category[]): { [databaseAlias: string]: number } {
    databases.forEach( d =>{
        let existingById = categories.find( c => c.id === d.id);
        if(existingById){
            existingById.name = d.alias;
        }
        else{
            categories.push({id:d.id, name: d.alias})
        }
    });

    const categoryMapping: { [alias: string]: number} = {};
    categories.forEach((c,i)=>categoryMapping[c.name]=i);

    return categoryMapping;
}

function getRelationFieldsPerAlias(metamodel: Metamodel) : { [databaseAlias: string]: [{id: string, databaseAlias: string}] } {
    let relationFieldsPerAlias: { [databaseAlias: string]: [{id: string, databaseAlias: string}]}= {};

    metamodel.edges.forEach( e => {
        let sourceAlias = e.from.alias;
        let targetAlias = e.to.alias;
        let sourceFieldLabel = e.navigability.forward?.label;
        let targetFieldLabel = e.navigability.reverse?.label;

        if(sourceAlias && sourceFieldLabel){
            let fieldsForAlias = relationFieldsPerAlias[sourceAlias];
            if(!fieldsForAlias){
                fieldsForAlias = [{id: sourceFieldLabel, databaseAlias: sourceAlias}]
                relationFieldsPerAlias[sourceAlias] = fieldsForAlias;
            }else if(!fieldsForAlias.some( f=>f.id === sourceFieldLabel )){
                fieldsForAlias.push({id: sourceFieldLabel, databaseAlias: sourceAlias});
            }
        }

        if(targetAlias && targetFieldLabel){
            let fieldsForAlias = relationFieldsPerAlias[targetAlias];
            if(!fieldsForAlias){
                fieldsForAlias = [{id: targetFieldLabel, databaseAlias: targetAlias}]
                relationFieldsPerAlias[sourceAlias] = fieldsForAlias;
            }else if(!fieldsForAlias.some(f=>f.id === targetFieldLabel)){
                fieldsForAlias.push({id: targetFieldLabel, databaseAlias: targetAlias});
            }
        }
    });

    return relationFieldsPerAlias;
}

function mapToDefinitionsByAlias(metamodel: Metamodel, databaseDefinitions: DatabaseDefinitions) {
    let result: { [databaseAlias: string]: DatabaseDefinition } = {};

    metamodel.databases.forEach( d => result[d.alias] = databaseDefinitions[d.id]);

    return result;
}