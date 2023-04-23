import {Action, combineReducers, configureStore, ThunkAction} from "@reduxjs/toolkit";
import queryExecutionReducer from "../features/querying/queryExecution-slice";
import queryHistoryReducer from "../features/querying/queryHistory-slice";
import loadMetamodelReducer from "../features/metamodel/metamodel-slice";
import metamodelLoadedListenerMiddleware from "../middleware/metamodel-load";
import dataStoreReducer from "../features/datastore/datastore-slice";
import dataStoreLoadListenerMiddleware from "../middleware/datastore-load";
import { createWrapper } from "next-redux-wrapper";
import storage from 'redux-persist/lib/storage'
import {persistReducer} from "redux-persist"; // defaults to localStorage for web


export const makeStore = ()=> {
    const reducers = combineReducers( {
        queryExecution: queryExecutionReducer,
        queryHistory: queryHistoryReducer,
        metamodel: loadMetamodelReducer,
        dataStore: dataStoreReducer
    });

    const persistConfig = {
        key: 'notionExplorer',
        storage,
        whitelist: ['queryHistory']
    };

    const persistedReducer = persistReducer(persistConfig, reducers);

    const store = configureStore({
        reducer: persistedReducer,
        devTools: process.env.NODE_ENV !== 'production',
        middleware: (getDefaultMiddleware) =>
            getDefaultMiddleware({serializableCheck: false})
                .prepend(dataStoreLoadListenerMiddleware.middleware)
                .prepend(metamodelLoadedListenerMiddleware.middleware),
    });

    return store;
};

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