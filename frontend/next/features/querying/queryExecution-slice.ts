import {createSlice, createAsyncThunk} from "@reduxjs/toolkit";
import notionApi from "../../notion-api";
import {QueryResult} from "../../notion-api/query";
import SuccessQueryExecutionResult from "../../notion-api/status/SuccessQueryExecutionResult";
import FailedQueryExecutionResult from "../../notion-api/status/FailedQueryExecutionResult";

interface QueryExecutionState {
    loading: 'idle' | 'pending' | 'succeeded' | 'failed',
    error: FailedQueryExecutionResult | null,
    queryResult: QueryResult | null
}

const initialState : QueryExecutionState = {
    loading: 'idle',
    error: null,
    queryResult: null
}

export const executeQuery = createAsyncThunk(
    'notion/query/execute',
    async (queryText: string)=>{
        return await notionApi.query.ExecuteQuery(queryText);
    });

export const analyzeQuery = createAsyncThunk(
    'notion/query/analyze',
    async (queryText: string) =>{
        return await notionApi.query.AnalyzeQuery(queryText);
    }
);

const queryExecutionSlice = createSlice({
    name: 'query-execution',
    initialState,
    reducers: {},
    extraReducers: builder => {
        builder.addCase(executeQuery.fulfilled, (state, action) =>{
            if(action.payload instanceof SuccessQueryExecutionResult){
                state.queryResult = (action.payload as SuccessQueryExecutionResult).result;
                state.error = null;
                state.loading = "succeeded";
            }else{
                state.queryResult = null;
                state.loading = "failed";
                state.error = (action.payload as FailedQueryExecutionResult);
            }
        });
        builder.addCase(executeQuery.rejected, (state, action) => {
            state.queryResult = null;
            state.loading = "failed";
            state.error = new FailedQueryExecutionResult("Unknown error, this should not happen", action.error, "");
        });
        builder.addCase(executeQuery.pending, (state)=> {
            state.error = null;
            state.queryResult = null;
            state.loading = "pending";
        });
    }
});

export default queryExecutionSlice.reducer;