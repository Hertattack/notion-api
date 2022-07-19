import React from "react";
import {QueryInput} from "./QueryInput";
import { useAppSelector } from "../app/hooks";
import { EChartsOptions, ReactECharts } from "./echarts/ECharts";
import {GraphNodeItemOption} from "echarts/types/src/chart/graph/GraphSeries";

export const FreeQuery : React.FC = () => {
    const { previousQueries } = useAppSelector( state => state.queryHistory );
    const { nodes, edges, categories } = useAppSelector( state=>state.dataStore.data );
    const { loading, queryResult } = useAppSelector(state=>state.queryExecution);

    const createGraphOptions = () : EChartsOptions => {
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
                    nodes: Object.getOwnPropertyNames(nodes).map( id => { let node = nodes[id]; return {
                        name: node.label,
                        symbolSize: 10,
                        category: node.category,
                        id: node.id
                    } as GraphNodeItemOption }),
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


    let options=  createGraphOptions();

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
            <div style={{width: "800px", height: "500px"}}><ReactECharts option={options}/></div>
            { queryResult !== null
                ? <div>
                    <br/>
                    <pre><code>{JSON.stringify(queryResult,null, 2)}</code></pre>
                </div>
                : '' }
        </div>
    </div> )
};

