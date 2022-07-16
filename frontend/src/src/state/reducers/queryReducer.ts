import { QueryActionType } from "../action-types";
import { QueryAction } from "../actions";

interface QueryState {
    executing: boolean,
    error: string | null,
    data: any
}

const initialState = {
    executing: false,
    error: null,
    data: null
}

const reducer = (
    state: QueryState = initialState,
    action: QueryAction): QueryState => {

    switch (action.type){
        case QueryActionType.EXECUTE_QUERY:
            return { executing: true, error: null, data: null };
        case QueryActionType.EXECUTE_QUERY_SUCCESS:
            return { executing: false, error: null, data: action.payload };
        case QueryActionType.EXECUTE_QUERY_FAILURE:
            return { executing: false, error: action.payload, data: null };
        default:
            return state;
    }
}

export default reducer;