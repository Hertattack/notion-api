import NavigationRole from "./NavigationRole.ts";

export default interface NavigabilitySpecification {
    forward?: NavigationRole,
    reverse?: NavigationRole
}
