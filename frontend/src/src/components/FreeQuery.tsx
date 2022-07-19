import React from "react";
import {QueryInput} from "./QueryInput";
import { useAppSelector } from "../app/hooks";
import { EChartsOptions, ReactECharts } from "./echarts/ECharts";
import {DatabaseReference, EdgeDefinition, Metamodel} from "../notion-api/interface/Metamodel";
import FieldIdentifier from "../notion-api/interface/FieldIdentifier";
import {DatabaseDefinition} from "../notion-api/interface/DatabaseDefinition";

interface LinkDefinition { label: string, alias: string, direction: 'forward'|'reverse'}

interface Database { reference: DatabaseReference; definition: DatabaseDefinition; links: LinkDefinition[] }

interface DatabaseMetadata {
    databases: Database[],
    properties: {
        [databaseAlias: string]: string[]
    }
}

interface DatabaseDefinitions { [databaseAlias: string] : DatabaseDefinition }

function filterFrom(edge: EdgeDefinition, database: Database, metamodel: Metamodel, returnedAliases: string[]) {
    if(!edge.from || !edge.to)
        return false;

    if(edge.from.alias !== database.reference.alias)
        return false;

    return !(!returnedAliases.some(a => a === edge.from.alias) || !returnedAliases.some(a => a === edge.to.alias));


}

function filterTo(edge: EdgeDefinition, database: Database, metamodel: Metamodel, returnedAliases: string[]) {
    if(!edge.from || !edge.to)
        return false;

    if(edge.to.alias !== database.reference.alias)
        return false;

    return !(!returnedAliases.some(a => a === edge.from.alias) || !returnedAliases.some(a => a === edge.to.alias));


}

function getDatabaseMetadata(
    metamodel: Metamodel,
    databaseDefinitions: DatabaseDefinitions,
    propertyNames: FieldIdentifier[]): DatabaseMetadata {
    let databases: string[] = [];
    let result: { [database: string]: string[] } = {};

    propertyNames.forEach( p => {
        if(p.alias === null)
            return;

        if(!result[p.alias]){
            databases.push(p.alias);
            result[p.alias] = [p.name];
        }else{
            result[p.alias].push(p.name);
        }
    });

    let returnedAliases: string[] = [];
    let metadata = {
        databases: databases.map(d => {
            let reference = metamodel.databases.find(md=>md.alias === d);
            if(!reference)
                throw new Error(`Could not find database: "${d}" in metamodel`);

            let definition = databaseDefinitions[reference.id];
            if(!definition)
                throw new Error(`Could not find database definition for database with alias: ${reference.alias}`);

            returnedAliases.push(reference.alias);

            let links: LinkDefinition[] = [];

            return {reference, definition, links};
        }),
        properties: result
    };

    metadata.databases.forEach( d => {
        let fromEdges = metamodel.edges.filter(e=>filterFrom(e, d, metamodel, returnedAliases))
            .map( e=>{return {label: e.navigability.forward?.label, alias: e.to.alias, direction: 'forward'}});
        let toEdges = metamodel.edges.filter(e=>filterTo(e, d, metamodel, returnedAliases))
            .map(e=>{return {label: e.navigability.reverse?.label, alias: e.from.alias, direction: 'reverse'}});

        // @ts-ignore
        d.links = [...fromEdges, ...toEdges]
    });

    return metadata;
}

interface Node {
    id: string,
    name: string,
    symbolSize: number,
    category: number
}

interface Edge {
    source: string | number,
    target: string | number
}

interface Category {
    id: string,
    name: string
}

export const FreeQuery : React.FC = () => {
    const { previousQueries } = useAppSelector( state => state.queryHistory )
    const { loading, queryResult } = useAppSelector( state=>state.queryExecution )
    const { loaded, metamodel, databaseDefinitions } = useAppSelector(state => state.metamodel)

    const createGraphOptions = (metamodel: Metamodel, databaseDefinitions: DatabaseDefinitions) : EChartsOptions => {
        let nodes: { [id: string]: Node } = {};
        let edges: { [fromTo: string]: Edge } = {};
        let categories: Category[] = [];

        if(queryResult != null) {
            let databaseMetadata = getDatabaseMetadata(metamodel, databaseDefinitions, queryResult.propertyNames);

            databaseMetadata.databases.forEach( d => { categories.push({name: d.definition.title, id: d.definition.id})});

            let categoryIds : { [databaseId: string] : number } = {};
            categories.forEach((c,i) => categoryIds[c.id] = i);

            let titleProperties: { [databaseId: string]: string } = {};
            databaseMetadata.databases.forEach( d => {
                let titleProperty = d.definition.properties.find(p=>p.type === 'title');
                if(titleProperty && databaseMetadata.properties[d.reference.alias].some( p => titleProperty && p === titleProperty.name))
                    titleProperties[d.definition.id] = titleProperty.name;
                else
                    titleProperties[d.definition.id] = "Id";
            });

            queryResult.rows.forEach(r => {
                databaseMetadata.databases.forEach( d => {
                    let pageId = r.fieldValueSets[d.reference.alias].values.Id
                    if(!nodes[pageId])
                        nodes[pageId] = {
                            name: r.fieldValueSets[d.reference.alias].values[titleProperties[d.reference.id]],
                            category: categoryIds[d.definition.id],
                            symbolSize: 20,
                            id: pageId
                        };

                    d.links.forEach( l => {
                        let linkedPageId = r.fieldValueSets[l.alias].values.Id;
                        let source = l.direction === 'forward' ? pageId : linkedPageId;
                        let target = l.direction === 'forward' ? linkedPageId : pageId;
                        edges[`${source}-->${target}`] = { source, target};
                    })
                });
            });
        }

        let nodeIndex: { [id: string]: number } = {};
        let nodeArray = Object.getOwnPropertyNames(nodes).map(id=>nodes[id]);

        nodeArray.forEach( (n,i)  => nodeIndex[n.id]=i);
        let edgeArray: Edge[] = [];
        Object.getOwnPropertyNames(edges).forEach(id=>{
            let edge = edges[id];
            let source = nodeIndex[edge.source];
            let target = nodeIndex[edge.target];

            if(source && target)
                edgeArray.push({ source , target });
        });

        return {
            tooltip: {},
            legend: [
                {
                    data: categories.map(c=>c.name)
                }
            ],
            series: [
                {
                    name: 'Les Miserables',
                    type: 'graph',
                    layout: 'force',
                    force: {
                        edgeLength : 500
                    },
                    data: nodeArray,
                    links: edgeArray,
                    categories: categories,
                    roam: true,
                    label: {
                        show: true,
                        position: 'right',
                        formatter: '{b}'
                    },
                    labelLayout: {
                        hideOverlap: true
                    },
                    scaleLimit: {
                        min: 0.4,
                        max: 20
                    },
                    lineStyle: {
                        color: 'source',
                        curveness: 0.3
                    }
                }
            ]
        };
    }


    let options: EChartsOptions | null = null;
    if(loaded && metamodel !== null && queryResult !== null)
        options = createGraphOptions(metamodel, databaseDefinitions);

    return (
    <div>
        <QueryInput/>
        <br/>
        <div>
            <ol>
                {previousQueries.map((pq)=>(
                    <li key={pq}>{pq}</li>
                ))}
            </ol>
        </div>
        { loading === 'pending' ? <div><p><b>query is being executed</b></p></div> : '' }
        <div id={"queryResult"}>
            { queryResult !== null
                ? <div>
                    {options !== null ? <div style={{width: "800px", height: "500px"}}><ReactECharts option={options}/></div> : ''}
                    <br/>
                    <pre><code>{JSON.stringify(queryResult,null, 2)}</code></pre>
                </div>
                : '' }
        </div>
    </div> )
};

