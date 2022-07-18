import {createListenerMiddleware} from "@reduxjs/toolkit";
import {loadMetamodel, errorUpdatingDatabaseDefinition, updateWithDatabaseDefinition} from "../features/metamodel/metamodel-slice";
import notionApi from "../notion-api";

const metamodelLoadedListenerMiddleware = createListenerMiddleware()

metamodelLoadedListenerMiddleware.startListening({
    actionCreator: loadMetamodel.fulfilled,
    effect: async (action, listenerApi) => {
        // Can cancel other running instances
        listenerApi.cancelActiveListeners()

        await Promise.all(action.payload.databases.map(async d => {
           try{
               let databaseDefinition = await notionApi.metamodel.GetDatabaseDefinition(d.alias);
               listenerApi.dispatch(updateWithDatabaseDefinition(databaseDefinition))
           }catch(e){
                listenerApi.dispatch(errorUpdatingDatabaseDefinition(d.alias));
           }
        }));
    },
});

export default metamodelLoadedListenerMiddleware;



