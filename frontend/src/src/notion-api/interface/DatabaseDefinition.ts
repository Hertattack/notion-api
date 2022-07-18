export interface DatabaseDefinition {
    id:  string,
    title: string,
    properties:[
        {
            name: string,
            type: string
        }
    ]
}