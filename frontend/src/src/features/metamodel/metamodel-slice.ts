import {createSlice, createAsyncThunk, PayloadAction} from "@reduxjs/toolkit";
import notionApi from "../../notion-api";
import {Metamodel} from "../../notion-api/interface/Metamodel";
import {DatabaseDefinition} from "../../notion-api/interface/DatabaseDefinition";

interface LoadMetamodelState {
    loaded: boolean,
    metamodel: Metamodel | null,
    databaseDefinitions: { [databaseAlias: string] : DatabaseDefinition }
}

const emptyModel : Metamodel = {
    databases: [],
    edges: []
}

const initialState : LoadMetamodelState = {
    loaded: false,
    metamodel: emptyModel,
    databaseDefinitions: {}
}

export const loadMetamodel = createAsyncThunk(
    'notion/metamodel',
    async ()=>{
        return await notionApi.metamodel.Get();
    });

const metamodelSlice = createSlice({
    name: 'metamodel',
    initialState,
    reducers: {
        updateWithDatabaseDefinition(state, action: PayloadAction<DatabaseDefinition>){
            state.databaseDefinitions[action.payload.id] = action.payload;
        },
        errorUpdatingDatabaseDefinition(state, action: PayloadAction<string>){
            console.log(`Could not update database definition for database: ${action.payload}`)
        }
    },
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


export const { errorUpdatingDatabaseDefinition, updateWithDatabaseDefinition } = metamodelSlice.actions;
export default metamodelSlice.reducer;