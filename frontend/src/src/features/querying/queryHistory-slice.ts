import { createSlice, PayloadAction } from "@reduxjs/toolkit";

const maxItemsInHistory = 50;

interface QueryHistoryState {
    previousQueries: string[]
}

const localStorageKey = 'QueryHistory';
let storedValue = localStorage.getItem(localStorageKey);
let defaultValue = storedValue ? JSON.parse(storedValue) as string[] : [];

const initialState : QueryHistoryState = {
    previousQueries: defaultValue
}

const queryHistorySlice = createSlice({
    name: 'query-history',
    initialState,
    reducers: {
        addQueryToHistory(state, action:  PayloadAction<string>) {
            if(!state.previousQueries.some(pq=>pq === action.payload)){
                let length = state.previousQueries.length;

                if(length > maxItemsInHistory)
                    state.previousQueries = state.previousQueries.slice(length - maxItemsInHistory);

                state.previousQueries.push(action.payload);

                localStorage.setItem(localStorageKey, JSON.stringify(state.previousQueries));
            }
        }
    }
});

export const { addQueryToHistory } = queryHistorySlice.actions;
export default queryHistorySlice.reducer;