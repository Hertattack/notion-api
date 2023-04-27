import DatabaseReference from "@/notion-api/model/metadata/DatabaseReference";
import EdgeDefinition from "@/notion-api/model/metadata/EdgeDefinition";


export default interface Metamodel {
    databases: DatabaseReference[],
    edges: EdgeDefinition[]
}