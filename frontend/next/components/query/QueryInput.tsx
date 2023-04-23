import React, {useEffect, useRef, useState} from "react";
import {useAppDispatch, useAppSelector} from "@/store/hooks";
import {executeQuery} from "@/features/querying/queryExecution-slice";
import {
    addQueryToHistory,
    firstQueryIndex,
    noSelection,
    selectQueryFromHistory
} from "@/features/querying/queryHistory-slice";

export const QueryInput : React.FC = () => {
    const [queryText, setQueryText] = useState('');
    const [selectedHistoryEntryIndex, setHistoryEntryIndex] = useState<number | null>(-1);
    const queryTextInputRef = useRef<HTMLInputElement | null>(null);

    const queryHistory = useAppSelector( state => state.queryHistory );
    const dispatch = useAppDispatch();

    useEffect(()=>{
        queryTextInputRef.current?.focus();
    },[]);

    if(selectedHistoryEntryIndex != queryHistory.selectedIndex){
        setHistoryEntryIndex(queryHistory.selectedIndex);
        if(queryHistory.selectedIndex != noSelection){
            const newText = queryHistory.previousQueries[queryHistory.selectedIndex];
            if(queryText !== newText)
                setQueryText(newText);
        }
    }

    function handleClick() {
        if(queryText.trim() === '')
            return;

        dispatch(addQueryToHistory(queryText));
        dispatch(executeQuery(queryText));
    }

    function searchHistory(e: React.KeyboardEvent<HTMLInputElement>) {
        const historyCount = queryHistory.previousQueries.length;
        if(historyCount == 0)
            return;

        const lastQueryIndex = historyCount - 1;
        let nextHistoryIndex = queryHistory.selectedIndex;

        console.log(e.key);
        if(e.key === 'ArrowUp'){
            if(nextHistoryIndex == noSelection)
                nextHistoryIndex = lastQueryIndex;
            else {
                nextHistoryIndex--;

                if (nextHistoryIndex < firstQueryIndex)
                    nextHistoryIndex = lastQueryIndex;
            }
        }else if(e.key === 'ArrowDown'){
            if(nextHistoryIndex === noSelection)
                nextHistoryIndex = firstQueryIndex;
            else{
                nextHistoryIndex++;

                if(nextHistoryIndex > lastQueryIndex)
                    nextHistoryIndex = firstQueryIndex;
            }
        }else{
            return;
        }

        if(nextHistoryIndex !== queryHistory.selectedIndex)
            dispatch(selectQueryFromHistory(nextHistoryIndex));
    }

    function handleChange(event : React.ChangeEvent<HTMLInputElement>) {
        setQueryText(event.target.value);
        dispatch(selectQueryFromHistory(noSelection));
    }

    function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
        event.preventDefault();
        handleClick();
    }

    return (
      <form onSubmit={handleSubmit}>
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