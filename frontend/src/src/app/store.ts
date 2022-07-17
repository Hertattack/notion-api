import { configureStore } from "@reduxjs/toolkit";
import queryExecutionReducer from "../features/querying/queryExecution-slice";
import queryHistoryReducer from "../features/querying/queryHistory-slice";
import loadMetamodelReducer from "../features/metamodel/metamodel-slice";

export const store = configureStore({
   reducer: {
       queryExecution: queryExecutionReducer,
       queryHistory: queryHistoryReducer,
       metamodel: loadMetamodelReducer
   }
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;