import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import notionApi from "../../notion-api";
import {Metamodel} from "../../notion-api/interface/Metamodel";

interface LoadMetamodelState {
    loaded: boolean,
    metamodel: Metamodel | null
}

const emptyModel : Metamodel = {
    databases: [],
    edges: []
}

const initialState : LoadMetamodelState = {
    loaded: false,
    metamodel: emptyModel
}

export const loadMetamodel = createAsyncThunk(
    'notion/metamodel',
    async ()=>{
        return await notionApi.metamodel.Get();
    });

const metamodelSlice = createSlice({
    name: 'metamodel',
    initialState,
    reducers: {},
    extraReducers: builder => {
        builder.addCase(loadMetamodel.fulfilled, (state, action) =>{
            state.loaded = true;
            state.metamodel = action.payload;
        });
        builder.addCase(loadMetamodel.rejected, (state) => {
            state.loaded = false;
            state.metamodel = emptyModel;
        });
        builder.addCase(loadMetamodel.pending, (state)=> {
            state.loaded = false;
            state.metamodel = emptyModel;
        });
    }
});

export default metamodelSlice.reducer;