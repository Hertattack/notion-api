import React, {useState} from "react";
import {QueryInput} from "./QueryInput";
import { useAppSelector } from "../app/hooks";
import {D3Graph, GraphOptions} from "./d3js/D3Graph";

export const FreeQuery : React.FC = () => {
    const { previousQueries } = useAppSelector( state => state.queryHistory );
    const { nodes, edges, categories } = useAppSelector( state=>state.dataStore.data );
    const { loading, queryResult, error } = useAppSelector(state=>state.queryExecution);

    let d3Nodes = nodes.map( n => { return {
        id: n.id,
        group: n.category
    }; } );

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
        { loading === 'failed' ?
            <div><p><b>Query has failed, message: {error !== null ? error.message : "No error message given"}</b></p>
            <p>{error != null ? error.data ?? "" : ""}</p>
            </div>
            :''}
        { loading === 'pending' ? <div><p><b>query is being executed</b></p></div> : '' }
        <div id={"queryResult"}>
            <div><D3Graph width={1400} height={1000} nodes={d3Nodes}/></div>
            { queryResult !== null
                ? <div>
                    <br/>
                    <pre><code>{JSON.stringify(queryResult,null, 2)}</code></pre>
                </div>
                : '' }
        </div>
    </div> )
};

