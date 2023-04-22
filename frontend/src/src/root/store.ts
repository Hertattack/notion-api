import { configureStore } from "@reduxjs/toolkit";
import queryExecutionReducer from "../features/querying/queryExecution-slice";
import queryHistoryReducer from "../features/querying/queryHistory-slice";
import loadMetamodelReducer from "../features/metamodel/metamodel-slice";
import metamodelLoadedListenerMiddleware from "../middleware/metamodel-load";
import dataStoreReducer from "../features/datastore/datastore-slice";
import dataStoreLoadListenerMiddleware from "../middleware/datastore-load";

export const store = configureStore({
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

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;