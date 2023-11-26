
export interface PropertyDefinition {
    name: string,
    type: string
}
export interface DatabaseDefinition {
    id:  string,
    title: string,
    properties: PropertyDefinition[];
}