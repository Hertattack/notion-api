import NodeReference from "@/notion-api/model/metadata/NodeReference";
import NavigabilitySpecification from "@/notion-api/model/metadata/NavigabilitySpecification";

export default interface EdgeDefinition {
    from: NodeReference,
    to: NodeReference,
    navigability: NavigabilitySpecification
}