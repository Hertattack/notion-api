import { QueryActionType } from "../action-types";

interface ExecuteQueryAction {
    type: QueryActionType.EXECUTE_QUERY
}

interface ExecuteQueryActionSuccess {
    type: QueryActionType.EXECUTE_QUERY_SUCCESS,
    payload: any
}

interface ExecuteQueryActionFailure {
    type: QueryActionType.EXECUTE_QUERY_FAILURE,
    payload: string
}

export type QueryAction =
    | ExecuteQueryAction
    | ExecuteQueryActionSuccess
    | ExecuteQueryActionFailure;