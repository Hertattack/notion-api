import {createAsyncThunk, createSlice} from "@reduxjs/toolkit";
import notionApi from "@/notion-api";
import FailedQueryExecutionResult from "@/notion-api/status/FailedQueryExecutionResult";
import {QueryPlan} from "@/notion-api/model/analysis/QueryPlan";
import SuccessQueryAnalysisResult from "@/notion-api/status/SuccessQueryAnalysisResult";

interface QueryAnalysisState {
    loading: 'idle' | 'pending' | 'succeeded' | 'failed',
    error: FailedQueryExecutionResult | null,
    analysisResult: QueryPlan | null
}

const initialState : QueryAnalysisState = {
    loading: 'idle',
    error: null,
    analysisResult: null
}

export const analyzeQuery = createAsyncThunk(
    'notion/query/analyze',
    async (queryText: string) =>{
        return await notionApi.query.AnalyzeQuery(queryText);
    }
);

const queryAnalysisSlice = createSlice({
    name: 'query-execution',
    initialState,
    reducers: {},
    extraReducers: builder => {
        builder.addCase(analyzeQuery.fulfilled, (state, action) =>{
            if(action.payload instanceof SuccessQueryAnalysisResult){
                state.analysisResult = (action.payload as SuccessQueryAnalysisResult).result;
                state.error = null;
                state.loading = "succeeded";
            }else{
                state.analysisResult = null;
                state.loading = "failed";
                state.error = (action.payload as FailedQueryExecutionResult);
            }
        });
        builder.addCase(analyzeQuery.rejected, (state, action) => {
            state.analysisResult = null;
            state.loading = "failed";
            state.error = new FailedQueryExecutionResult("Unknown error, this should not happen", action.error, "");
        });
        builder.addCase(analyzeQuery.pending, (state)=> {
            state.error = null;
            state.analysisResult = null;
            state.loading = "pending";
        });
    }
});

export default queryAnalysisSlice.reducer;