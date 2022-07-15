import React, {useState} from "react";

export const FreeQuery : React.FC = () => {
    const [queryText, setQueryText] = useState('');
    const [previousQueries, setPreviousQueries] = useState<string[]>([]);
    const [historyIndex, setHistoryIndex] = useState(0);
    const [queryResult, setQueryResult] = useState({});

    function click() {
        if(!previousQueries.some(pq=>pq == queryText)){
            setPreviousQueries([...previousQueries, queryText]);
            setHistoryIndex(previousQueries.length);
        }

        let uri = `https://localhost:7136/Query?query=${encodeURIComponent(queryText)}`;

        fetch(uri)
            .then(r => r.json())
            .then(d => setQueryResult(d))
            .catch( e => alert(e));
    }

    function searchHistory(e: React.KeyboardEvent<HTMLInputElement>) {
        let updatedHistoryIndex : number = -1;

        if(e.key == 'ArrowUp' && historyIndex > 0){
                updatedHistoryIndex = historyIndex - 1;
        }else if(e.key == 'ArrowDown' && historyIndex < previousQueries.length - 1){
            updatedHistoryIndex = historyIndex + 1;
        }

        if(updatedHistoryIndex >= 0){
            setHistoryIndex(updatedHistoryIndex);
            setQueryText(previousQueries[updatedHistoryIndex]);
            (document.getElementById("queryText") as HTMLInputElement).value = queryText;
        }
    }

    function handleChange(event : React.ChangeEvent<HTMLInputElement>) {
        setQueryText(event.target.value);
    }

    return <div>
        <form>
            <input type={"text"} size={100} id={"queryString"} value={queryText} onChange={handleChange} onKeyUp={e=>searchHistory(e)}/><button type={"button"} onClick={click}>Click me</button>
        </form>
        <br/>
        <div>
            {historyIndex}
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

