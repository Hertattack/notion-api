import { saveState as qhSaveState, loadState as qhLoadState} from "../features/querying/queryHistory-storage";

export function loadPersistedState(){
    try{
        return {
            queryHistory : qhLoadState()
        };
    }catch(e){
        return undefined;
    }
}

export async function persistState(state: any) {
    try{
        await qhSaveState(state);
    }catch(e){
        // Do nothing.
    }
}