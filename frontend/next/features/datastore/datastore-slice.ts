import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {DatabaseDefinitions} from "../metamodel/metamodel-slice";
import FieldIdentifier from "@/notion-api/model/FieldIdentifier";
import {DatabaseDefinition} from "@/notion-api/model/DatabaseDefinition";
import QueryResult from "@/notion-api/model/QueryResult";
import Metamodel from "@/notion-api/model/metadata/Metamodel";
import DatabaseReference from "@/notion-api/model/metadata/DatabaseReference";

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
    source: number,
    target: number,
    roles: string[]
}

interface DataStore {
    nodeIndex: {[nodeId: string]: number}
    nodes: Node[],
    categories: Category[],
    edges: Edge[]
}

export interface DataStoreState {
    data: DataStore
}

const initialState: DataStoreState = {
    data: {
        nodeIndex: {},
        nodes: [],
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
        let {nodeIndex, nodes,categories, edges} = state.data;
        const {queryResult, metamodel, databaseDefinitions} = action.payload;

        const categoryMapping = updateCategories(metamodel.databases, categories);
        const databaseAliases = mapPropertyNamesToDatabases(queryResult.propertyNames);
        const databaseDefinitionsByAlias: { [databaseAlias: string]: DatabaseDefinition } = mapToDefinitionsByAlias(metamodel, databaseDefinitions);
        const relationsPerDatabaseAlias = getRelationFieldsPerAlias(metamodel);
        const titleFields: {[databaseAlias: string]: string|undefined} = {};
        databaseAliases.forEach(a=> titleFields[a] = databaseDefinitionsByAlias[a].properties.find(p=>p.type==='title')?.name);

        let nodeIds: string[] = [];
        let updatedNodes: {[nodeId: string]: Node} = {}
        let potentialRelations: {sourceId: string, targetId: string, role: string | undefined}[]  = [];
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

                if(relationsPerDatabaseAlias[d]!==undefined) {
                    relationsPerDatabaseAlias[d].forEach(r => {
                        let fieldValue = valueSet[r.id] as string[];
                        if (fieldValue !== undefined) {
                            fieldValue.forEach(v => potentialRelations.push({sourceId: pageId, targetId: v, role: r.role}));
                        }
                    });
                }
            });
        });

        updateAndInsertNodes(nodeIds, updatedNodes, nodes, nodeIndex);
        potentialRelations.filter((r, i, self) =>
                !self.some( (v,vi) => v.sourceId === r.sourceId && v.targetId === r.targetId && i > vi) )
            .forEach( r =>{
                let sourceIndex = nodeIndex[r.sourceId];
                let targetIndex = nodeIndex[r.targetId];

                if(sourceIndex === undefined || targetIndex === undefined)
                    return;

                const edge = edges.find(e=>e.target === targetIndex && e.source === sourceIndex);
                if(edge === undefined)
                    edges.push({ source: sourceIndex, target: targetIndex, roles: [r.role ?? "undefined"] });
                else{
                    if(!edge.roles.some(role=>role == r.role ?? "undefined"))
                        edge.roles.push(r.role ?? "undefined");
                }
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

function getRelationFieldsPerAlias(metamodel: Metamodel) : { [p: string]: [{ id: string; databaseAlias: string, role: string | undefined }] } {
    let relationFieldsPerAlias: { [databaseAlias: string]: [{id: string, databaseAlias: string, role: string | undefined}]}= {};

    metamodel.edges.forEach( e => {
        let fromAlias = e.from.alias;
        let toAlias = e.to.alias;
        let forwardRole = e.navigability.forward?.role;
        let forwardFieldLabel = e.navigability.forward?.label;
        let reverseRole = e.navigability.forward?.role;
        let reverseFieldLabel = e.navigability.reverse?.label;

        if(fromAlias && forwardFieldLabel){
            let fieldsForAlias = relationFieldsPerAlias[fromAlias];
            if(!fieldsForAlias){
                fieldsForAlias = [{id: forwardFieldLabel, databaseAlias: toAlias, role: forwardRole}]
                relationFieldsPerAlias[fromAlias] = fieldsForAlias;
            }else if(!fieldsForAlias.some( f=>f.id === forwardFieldLabel )){
                fieldsForAlias.push({id: forwardFieldLabel, databaseAlias: toAlias, role: forwardRole});
            }
        }

        if(toAlias && reverseFieldLabel){
            let fieldsForAlias = relationFieldsPerAlias[toAlias];
            if(!fieldsForAlias){
                fieldsForAlias = [{id: reverseFieldLabel, databaseAlias: fromAlias, role: reverseRole}]
                relationFieldsPerAlias[toAlias] = fieldsForAlias;
            }else if(!fieldsForAlias.some(f=>f.id === reverseFieldLabel)){
                fieldsForAlias.push({id: reverseFieldLabel, databaseAlias: fromAlias, role: reverseRole});
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

function updateAndInsertNodes(nodeIds: string[], updatedNodes: { [p: string]: Node }, nodes: Node[], nodeIndex: {[id: string]: number }) {
    let nextNodeIndex = nodes.length;
    nodeIds.forEach( id => {
        let existingNodeIndex = nodeIndex[id];

        if(existingNodeIndex!==undefined){
            nodes[existingNodeIndex] = updatedNodes[id];
        }else{
            nodes.push(updatedNodes[id]);
            nodeIndex[id] = nextNodeIndex;
            nextNodeIndex++;
        }
    });
}