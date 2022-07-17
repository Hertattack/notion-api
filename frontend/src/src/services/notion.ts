import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'

export const notionApi = createApi({
    reducerPath: 'notionApi',
    baseQuery: fetchBaseQuery({ baseUrl: 'https://localhost:7136/' }),
    endpoints: (builder) => ({
        execute: builder.query<any, string>({
            query: (queryText) => `Query/?query=${queryText}`,
        }),
    }),
});

export const { useExecuteQuery } = notionApi;