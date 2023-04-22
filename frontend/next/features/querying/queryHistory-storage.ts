import {createAsyncThunk} from "@reduxjs/toolkit";

const localStorageKey = 'QueryHistory';

export interface QueryHistoryState {
    previousQueries: string[]
}

export const initialState : QueryHistoryState = {
    previousQueries: []
}

export function loadState() {
    const state = initialState;

    try {
        const serializedState = localStorage.getItem(localStorageKey);
        state.previousQueries = !serializedState ? [] as string[] : JSON.parse(serializedState) as string[];
    } catch (e) {
        // Ignore
    }

    return state;
}

const z = createAsyncThunk(
    'query/history',
    async() => {

    });

export async function saveState(state: any) {
    try {
        localStorage.setItem(localStorageKey, JSON.stringify(state.previousQueries));
    } catch (e) {
        // Ignore
    }
}
