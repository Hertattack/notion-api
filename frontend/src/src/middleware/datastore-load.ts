import {createListenerMiddleware} from "@reduxjs/toolkit";
import {executeQuery} from "../features/querying/queryExecution-slice";
import {updateDataStore} from "../features/datastore/datastore-slice";
import {RootState} from "../app/store";

const dataStoreLoadListenerMiddleware = createListenerMiddleware();

dataStoreLoadListenerMiddleware.startListening({
    actionCreator: executeQuery.fulfilled,
    effect: (action, listenerApi)=>{
        const {metamodel, databaseDefinitions} = (listenerApi.getState() as RootState).metamodel;

        if(metamodel === null || !metamodel.databases.some(d=>databaseDefinitions[d.id]))
            return;

        listenerApi.dispatch(updateDataStore({ queryResult: action.payload, metamodel, databaseDefinitions }))
    }
});

export default dataStoreLoadListenerMiddleware;