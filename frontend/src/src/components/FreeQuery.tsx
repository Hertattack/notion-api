import React, {useState} from "react";
import {QueryInput} from "./QueryInput";

export const FreeQuery : React.FC = () => {
    const [previousQueries, setPreviousQueries] = useState<string[]>([]);
    const [queryResult, setQueryResult] = useState({});

    function performQuery(queryText : string) {
        if(!previousQueries.some(pq=>pq == queryText)){
            setPreviousQueries([...previousQueries, queryText]);
        }

        let uri = `https://localhost:7136/Query?query=${encodeURIComponent(queryText)}`;

        fetch(uri)
            .then(r => r.json())
            .then(d => setQueryResult(d))
            .catch( e => alert(e));
    }

    return <div>
        <QueryInput executeQueryCallback={performQuery} queryHistory={previousQueries}/>
        <br/>
        <div>
            <ol>
                {previousQueries.map((pq)=>(
                    <li key={pq}>{pq}</li>
                ))}
            </ol>
        </div>
        <div id={"queryResult"}>
        <pre>
            <code>
            {JSON.stringify(queryResult,null, 2)}
            </code>
        </pre>
        </div>
  </div>
};

