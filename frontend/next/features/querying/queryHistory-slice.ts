import {createAsyncThunk, createSlice, PayloadAction} from "@reduxjs/toolkit";
const maxItemsInHistory = 50;

export const noSelection = -1;
export const firstQueryIndex = 0;

export interface QueryHistoryState {
    previousQueries: string[],
    selectedIndex: number
}

export const initialState : QueryHistoryState = {
    previousQueries: [],
    selectedIndex: noSelection
}

export const selectQueryFromHistory = createAsyncThunk(
'query/history/select',
(selectedIndex : number)=>{
    return selectedIndex;
});

export const deleteSelectedQueryFromHistory = createAsyncThunk(
    'query/history/delete-selected',
    ()=>{
        return true;
    });

export const clearQueryHistory = createAsyncThunk(
'query/history/clear',
()=>{
        return true;
    });

const queryHistorySlice = createSlice({
    name: 'query/history',
    initialState,
    reducers: {
        addQueryToHistory: function (state, action: PayloadAction<string>) {
            const indexOfQueryText = state.previousQueries.indexOf(action.payload);
            if (indexOfQueryText == noSelection) {
                let length = state.previousQueries.length;

                if (length > maxItemsInHistory)
                    state.previousQueries = state.previousQueries.slice(length - maxItemsInHistory);

                state.previousQueries.push(action.payload);
                state.selectedIndex = state.previousQueries.length-1;
            }else{
                state.selectedIndex = indexOfQueryText;
            }
        }
    },
    extraReducers: builder => {
        builder.addCase(selectQueryFromHistory.fulfilled, (state, action)=>{
            if(action.payload == noSelection){
                state.selectedIndex = action.payload;
                return;
            }

            if(state.previousQueries.length >= action.payload)
                state.selectedIndex = action.payload;
            else
                state.selectedIndex = noSelection;
        });
        builder.addCase(deleteSelectedQueryFromHistory.fulfilled, (state)=>{
            if(state.selectedIndex == noSelection)
                return;

            state.previousQueries.splice(state.selectedIndex,1);
            state.selectedIndex = noSelection;
        });
        builder.addCase(clearQueryHistory.fulfilled, (state)=>{
            state.selectedIndex = noSelection;
            state.previousQueries = [];
        });
    }
});

export const { addQueryToHistory } = queryHistorySlice.actions;
export default queryHistorySlice.reducer;