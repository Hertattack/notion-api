import {createListenerMiddleware} from "@reduxjs/toolkit";
import {executeQuery} from "@/features/querying/queryExecution-slice";
import {updateDataStore} from "@/features/datastore/datastore-slice";
import SuccessQueryExecutionResult from "../notion-api/status/SuccessQueryExecutionResult";
import {AppState} from "@/store/store";

const dataStoreLoadListenerMiddleware = createListenerMiddleware();

dataStoreLoadListenerMiddleware.startListening({
    actionCreator: executeQuery.fulfilled,
    effect: (action, listenerApi)=>{
        const {metamodel, databaseDefinitions} = (listenerApi.getState() as AppState).metamodel;

        if(metamodel === null || !metamodel.databases.some(d=>databaseDefinitions[d.id]))
            return;

        if(action.payload instanceof SuccessQueryExecutionResult){
            const executionResult = action.payload as SuccessQueryExecutionResult;
            listenerApi.dispatch(updateDataStore({ queryResult: executionResult.result, metamodel, databaseDefinitions }))
        }
    }
});

export default dataStoreLoadListenerMiddleware;