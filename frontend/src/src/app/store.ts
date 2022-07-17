import { configureStore } from "@reduxjs/toolkit";
import queryExecutionReducer from "../features/querying/queryExecution-slice";
import queryHistoryReducer from "../features/querying/queryHistory-slice";

export const store = configureStore({
   reducer: {
       queryExecution: queryExecutionReducer,
       queryHistory: queryHistoryReducer
   }
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;