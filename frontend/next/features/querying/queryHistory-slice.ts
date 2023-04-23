import {createAsyncThunk, createSlice, PayloadAction} from "@reduxjs/toolkit";
const maxItemsInHistory = 50;

export interface QueryHistoryState {
    previousQueries: string[],
    selectedIndex: number
}

export const initialState : QueryHistoryState = {
    previousQueries: [],
    selectedIndex: -1
}

export const selectQueryFromHistory = createAsyncThunk(
'query/history',
(selectedIndex : number)=>{
    return selectedIndex;
});


const queryHistorySlice = createSlice({
    name: 'query/history',
    initialState,
    reducers: {
        addQueryToHistory: function (state, action: PayloadAction<string>) {
            const indexOfQueryText = state.previousQueries.indexOf(action.payload);
            if (indexOfQueryText == -1) {
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
            if(action.payload == -1){
                state.selectedIndex = action.payload;
                return;
            }

            if(state.previousQueries.length >= action.payload)
                state.selectedIndex = action.payload;
            else
                state.selectedIndex = -1;
        });
    }
});

export const { addQueryToHistory } = queryHistorySlice.actions;
export default queryHistorySlice.reducer;