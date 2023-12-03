import NodeReference from "./NodeReference.ts";
import NavigabilitySpecification from "./NavigabilitySpecification.ts";

export default interface EdgeDefinition {
    from: NodeReference,
    to: NodeReference,
    navigability: NavigabilitySpecification
}