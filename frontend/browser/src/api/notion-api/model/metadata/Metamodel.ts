import DatabaseReference from "./DatabaseReference.ts";
import EdgeDefinition from "./EdgeDefinition.ts";


export default interface Metamodel {
    databases: DatabaseReference[],
    edges: EdgeDefinition[]
}