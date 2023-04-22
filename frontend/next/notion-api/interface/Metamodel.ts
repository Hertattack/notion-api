
export interface DatabaseReference {
    id: string,
    alias: string
}

export interface NodeReference {
    type: 'database',
    alias: string
}

export interface NavigationRole {
    role: string,
    label:string
}

export interface NavigabilitySpecification {
    forward?: NavigationRole,
    reverse?: NavigationRole
}

export interface EdgeDefinition {
    from: NodeReference,
    to: NodeReference,
    navigability: NavigabilitySpecification
}

export interface Metamodel {
    databases: DatabaseReference[],
    edges: EdgeDefinition[]
}