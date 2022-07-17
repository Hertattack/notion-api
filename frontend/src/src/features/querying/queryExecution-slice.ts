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
        builder.addCase(executeQuery.rejected, (state, action) => {
           state.queryResult = null;
           console.log(typeof action.payload);
           state.loading = "failed";
        });
    }
});

export default queryExecutionSlice.reducer;