import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import notionApi from "../../notion-api";
import {QueryResult} from "../../notion-api/query";
import RequestError from "../../notion-api/RequestError";

interface QueryExecutionState {
    loading: 'idle' | 'pending' | 'succeeded' | 'failed',
    error: RequestError | string | null,
    queryResult: QueryResult | null
}

const initialState : QueryExecutionState = {
    loading: 'idle',
    error: null,
    queryResult: null
}

export const executeQuery = createAsyncThunk(
    'notion/query',
    async (queryText: string)=>{
        return await notionApi.query.ExecuteQuery(queryText);
    });

const queryExecutionSlice = createSlice({
    name: 'query-execution',
    initialState,
    reducers: {},
    extraReducers: builder => {
        builder.addCase(executeQuery.fulfilled, (state, action) =>{
            state.queryResult = action.payload;
            state.error = null;
            state.loading = "succeeded";
        });
        builder.addCase(executeQuery.rejected, (state) => {
           state.queryResult = null;
           state.loading = "failed";
        });
        builder.addCase(executeQuery.pending, (state)=> {
            state.error = null;
            state.queryResult = null;
            state.loading = "pending";
        });
    }
});

export default queryExecutionSlice.reducer;