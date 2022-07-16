import { combineReducers } from "redux";
import queryReducer from "./queryReducer";

const reducers = combineReducers({
    queryState: queryReducer
});

export default reducers;

export type RootState = ReturnType<typeof reducers>;