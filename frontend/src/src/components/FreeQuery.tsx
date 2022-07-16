import React, {useState} from "react";
import {QueryInput} from "./QueryInput";
import {useLocalStorage} from "../util/storage";

const maxItemsInHistory = 50;

export const FreeQuery : React.FC = () => {
    const [previousQueries, setPreviousQueries] = useLocalStorage<string[]>('queryHistory',[]);
    const [queryResult, setQueryResult] = useState({});

    function performQuery(queryText : string) {
        if(!previousQueries.some(pq=>pq === queryText)){
            let length = previousQueries.length;

            if(length > maxItemsInHistory)
                setPreviousQueries([...previousQueries.slice(length - maxItemsInHistory), queryText]);
            else
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

