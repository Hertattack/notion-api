import React from "react";
import {QueryInput} from "./QueryInput";
import { useAppSelector } from "../app/hooks";
import { EChartsOptions, ReactECharts } from "./echarts";
import {DatabaseDefinition, Metamodel} from "../notion-api/interface/Metamodel";
import FieldIdentifier from "../notion-api/interface/FieldIdentifier";

interface DatabaseProperties {
    databases: DatabaseDefinition[],
    properties: {
        [databaseAlias: string]: string[]
    }
}

function getDatabaseProperties(metamodel: Metamodel, propertyNames: FieldIdentifier[]): DatabaseProperties {
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

    return {
        databases: databases.map(d => {
            let definition = metamodel.databases.find(md=>md.alias === d);
            if(!definition)
                throw new Error(`Could not find database: "${d}" in metamodel`);

            return definition;
        }),
        properties: result
    };
}

interface Node {
    id: string,
    name: string,
    symbolSize: number,
    category: number
}

interface Category {
    name: string
}

export const FreeQuery : React.FC = () => {
    const { previousQueries } = useAppSelector( state => state.queryHistory )
    const { loading, queryResult } = useAppSelector( state=>state.queryExecution )
    const metamodelState = useAppSelector(state => state.metamodel)


    const createGraphOptions = (metamodel: Metamodel) : EChartsOptions => {
        let nodes: Node[] = [];
        let edges: object[] = [];
        let categories: Category[] = [{name:"Documents"}];

        if(queryResult != null) {
            let databaseProperties = getDatabaseProperties(metamodel, queryResult.propertyNames);

            queryResult.rows.forEach(r => {
                databaseProperties.databases.forEach( d => {
                    nodes.push({
                        name: r.fieldValueSets[d.alias].values.Name,
                        category: 0,
                        symbolSize: 20,
                        id: r.fieldValueSets[d.alias].values.Id
                    });
                });
            });
        }


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
                    data: nodes,
                    links: edges,
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
    if(metamodelState.metamodel !== null && queryResult !== null)
        options = createGraphOptions(metamodelState.metamodel);

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

