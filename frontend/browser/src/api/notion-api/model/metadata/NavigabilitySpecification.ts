import NavigationRole from "@/notion-api/model/metadata/NavigationRole";

export default interface NavigabilitySpecification {
    forward?: NavigationRole,
    reverse?: NavigationRole
}
