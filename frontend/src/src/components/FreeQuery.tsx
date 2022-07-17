import React from "react";
import {QueryInput} from "./QueryInput";
import { useAppSelector } from "../app/hooks";

export const FreeQuery : React.FC = () => {
    const { previousQueries } = useAppSelector( state => state.queryHistory )
    const { queryResult } = useAppSelector( state=>state.queryExecution )

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
        <div id={"queryResult"}>
            {queryResult !== null ?
            <pre>
                <code>
                {JSON.stringify(queryResult,null, 2)}
                </code>
            </pre>
                : <p>Nothing</p>}
        </div>
    </div> )
};

