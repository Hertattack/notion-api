import { createSlice, PayloadAction } from "@reduxjs/toolkit";
const maxItemsInHistory = 50;

export interface QueryHistoryState {
    previousQueries: string[]
}

export const initialState : QueryHistoryState = {
    previousQueries: []
}

const queryHistorySlice = createSlice({
    name: 'query/history',
    initialState,
    reducers: {
        addQueryToHistory: function (state, action: PayloadAction<string>) {
            if (!state.previousQueries.some(pq => pq === action.payload)) {
                let length = state.previousQueries.length;

                if (length > maxItemsInHistory)
                    state.previousQueries = state.previousQueries.slice(length - maxItemsInHistory);

                state.previousQueries.push(action.payload);
            }
        }
    }
});

export const { addQueryToHistory } = queryHistorySlice.actions;
export default queryHistorySlice.reducer;