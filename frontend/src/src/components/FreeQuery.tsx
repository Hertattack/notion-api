import React from "react";
import {QueryInput} from "./QueryInput";
import { useAppSelector } from "../app/hooks";

export const FreeQuery : React.FC = () => {
    const { previousQueries } = useAppSelector( state => state.queryHistory )
    const { loading, queryResult } = useAppSelector( state=>state.queryExecution )

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
                ? <pre><code>{JSON.stringify(queryResult,null, 2)}</code></pre>
                : '' }
        </div>
    </div> )
};

