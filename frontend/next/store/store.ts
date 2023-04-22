import {Action, configureStore, ThunkAction} from "@reduxjs/toolkit";
import queryExecutionReducer from "../features/querying/queryExecution-slice";
import queryHistoryReducer from "../features/querying/queryHistory-slice";
import loadMetamodelReducer from "../features/metamodel/metamodel-slice";
import metamodelLoadedListenerMiddleware from "../middleware/metamodel-load";
import dataStoreReducer from "../features/datastore/datastore-slice";
import dataStoreLoadListenerMiddleware from "../middleware/datastore-load";
import { createWrapper } from "next-redux-wrapper";

export const makeStore = ()=> configureStore({
    reducer: {
        queryExecution: queryExecutionReducer,
        queryHistory: queryHistoryReducer,
        metamodel: loadMetamodelReducer,
        dataStore: dataStoreReducer
    },
    middleware: (getDefaultMiddleware) =>
        getDefaultMiddleware()
            .prepend(dataStoreLoadListenerMiddleware.middleware)
            .prepend(metamodelLoadedListenerMiddleware.middleware),
});



export type AppStore = ReturnType<typeof makeStore>;
export type AppState = ReturnType<AppStore["getState"]>;
export type AppDispatch = AppStore["dispatch"];
export type AppThunk<ReturnType = void> = ThunkAction<
    ReturnType,
    AppState,
    unknown,
    Action
>;
export const wrapper = createWrapper<AppStore>(makeStore);