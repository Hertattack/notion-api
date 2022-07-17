import React, {useEffect, useRef, useState} from "react";
import {useAppDispatch, useAppSelector} from "../app/hooks";
import {executeQuery} from "../features/querying/queryExecution-slice";
import {addQueryToHistory} from "../features/querying/queryHistory-slice";

export const QueryInput : React.FC = () => {
    const [queryText, setQueryText] = useState('');
    const  [selectedHistoryEntryIndex, setHistoryEntryIndex] = useState<number | null>(null);
    const queryTextInputRef = useRef<HTMLInputElement | null>(null);

    const queryHistory = useAppSelector( state => state.queryHistory.previousQueries );
    const dispatch = useAppDispatch();

    useEffect(()=>{
        queryTextInputRef.current?.focus();
    },[]);

    function handleClick() {
        if(queryText.trim() === '')
            return;

        if(selectedHistoryEntryIndex && queryHistory[selectedHistoryEntryIndex] !== queryText)
            setHistoryEntryIndex(null);

        dispatch(addQueryToHistory(queryText));
        dispatch(executeQuery(queryText));
    }

    function searchHistory(e: React.KeyboardEvent<HTMLInputElement>) {
        let updatedHistoryEntryIndex = (selectedHistoryEntryIndex ? selectedHistoryEntryIndex : 0);

        if(e.key === 'ArrowUp'){
            updatedHistoryEntryIndex--;
        }else if(e.key === 'ArrowDown'){
            updatedHistoryEntryIndex++;
        }

        if(updatedHistoryEntryIndex !== selectedHistoryEntryIndex
            && updatedHistoryEntryIndex >= 0 && updatedHistoryEntryIndex < queryHistory.length){
            setHistoryEntryIndex(updatedHistoryEntryIndex);
            setQueryText(queryHistory[updatedHistoryEntryIndex]);

            if(queryTextInputRef.current)
                queryTextInputRef.current.value = queryText;
        }
    }

    function handleChange(event : React.ChangeEvent<HTMLInputElement>) {
        setQueryText(event.target.value);
    }

    return (
      <form>
          <input
              ref={queryTextInputRef}
              type={"text"}
              size={100}
              id={"queryString"}
              value={queryText}
              onChange={handleChange}
              onKeyUp={e=>searchHistory(e)}/>
          <button type={"button"} onClick={handleClick}>Execute</button>
      </form>
    );
}