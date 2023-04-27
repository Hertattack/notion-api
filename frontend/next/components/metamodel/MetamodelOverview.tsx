import React from "react";
import {useAppSelector} from "@/store/hooks";
import {DatabaseDefinitions} from "@/features/metamodel/metamodel-slice";
import {Accordion, Col, ListGroup, ListGroupItem, Row} from "react-bootstrap";
import {PropertyDefinition} from "@/notion-api/model/DatabaseDefinition";
import {
    ArrowDownUp, ArrowLeft,
    ArrowLeftRight,
    ArrowLeftSquareFill,
    ArrowRight,
    ArrowRightSquareFill, QuestionOctagonFill
} from "react-bootstrap-icons";
import EdgeDefinition from "@/notion-api/model/metadata/EdgeDefinition";
import Metamodel from "@/notion-api/model/metadata/Metamodel";
import DatabaseReference from "@/notion-api/model/metadata/DatabaseReference";

export const MetamodelOverview : React.FC = ()=>{
    const metamodel = useAppSelector(state => state.metamodel);

    return (
        <div className="metamodel-overview">
            { !metamodel.loaded || metamodel.metamodel === null
                ? "Metamodel not loaded yet."
                : GenerateOverview(metamodel.metamodel, metamodel.databaseDefinitions)}
        </div>
    )
}

function CreateAccordionItem(metamodel: Metamodel, databaseDefinitions: DatabaseDefinitions, reference : DatabaseReference, index: number ) : JSX.Element{
    const definition = databaseDefinitions[reference.id];
    const edges = metamodel.edges.filter( e => e.from.alias == reference.alias || e.to.alias == reference.alias);
    function CreatePropertyItem(property: PropertyDefinition): JSX.Element {
        return (
            <ListGroupItem>{property.name} ({property.type})</ListGroupItem>
        )
    }

    function CreateEdgeItem(alias: string, edge: EdgeDefinition) : JSX.Element {
        let edgeIcon : JSX.Element;
        let description : string;

        const selfReference = edge.from.alias == alias && edge.to.alias == alias;

        if(edge.navigability.reverse !== undefined && edge.navigability.forward !== undefined) {
            if(selfReference) {
                edgeIcon = (<ArrowDownUp className="float-left edge-icon"/>);
                description = `(${edge.from.alias})<-[ ${edge.navigability.reverse.role} | ${edge.navigability.forward.role} ]->(${edge.to.alias})`;
            }else {
                edgeIcon = (<ArrowLeftRight className="float-left edge-icon"/>);
                description = `(${edge.from.alias})<-[ ${edge.navigability.reverse.role} | ${edge.navigability.forward.role} ]->(${edge.to.alias})`;
            }
        }else if(edge.navigability.forward !== undefined) {
            if(selfReference) {
                edgeIcon = (<ArrowRightSquareFill className="float-left edge-icon"/>);
                description = `(${edge.from.alias})-[ ${edge.navigability.forward.role} ]->(${edge.to.alias})`;
            }else {
                edgeIcon = (<ArrowRight className="float-left edge-icon"/>);
                description = `(${edge.from.alias})-[ ${edge.navigability.forward.role} ]->(${edge.to.alias})`;
            }
        }else if(edge.navigability.reverse !== undefined){
            if(selfReference){
                edgeIcon = (<ArrowLeftSquareFill className="float-left edge-icon"/>);
                description = `(${edge.from.alias})<-[ ${edge.navigability.reverse.role} ]-(${edge.to.alias})`;
            }else{
                edgeIcon = (<ArrowLeft className="float-left edge-icon"/>);
                description = `(${edge.from.alias})<-[ ${edge.navigability.reverse.role} ]-(${edge.to.alias})`;
            }
        }else{
            edgeIcon = (<QuestionOctagonFill className="float-left edge-icon"/>);
            description = `(${edge.from.alias})-[ ? ]-(${edge.to.alias})`;
        }


        return (
            <ListGroupItem>
                <div className="edge-definition"><Row><Col md={1}>{edgeIcon}</Col><Col>{description}</Col></Row></div>
            </ListGroupItem>
        )
    }

    return (
        <Accordion.Item eventKey={index.toString()}>
            <Accordion.Header>{definition.title} ({reference.alias})</Accordion.Header>
            <Accordion.Body>
                <Accordion>
                    <Accordion.Item eventKey="properties">
                        <Accordion.Header title="Properties">Properties</Accordion.Header>
                        <Accordion.Body>
                            <ListGroup>
                                {definition.properties.map(CreatePropertyItem)}
                            </ListGroup>
                        </Accordion.Body>
                    </Accordion.Item>
                    <Accordion.Item eventKey="relateions">
                        <Accordion.Header title="Relations">Relations <div className="relation-code"><code>{"(from)<-[ fromRole | toRole ]->(to)"}</code></div></Accordion.Header>
                        <Accordion.Body>
                            <ListGroup>
                                {edges.map((e)=>CreateEdgeItem(reference.alias, e))}
                            </ListGroup>
                        </Accordion.Body>
                    </Accordion.Item>
                </Accordion>
            </Accordion.Body>
        </Accordion.Item>
    )
}

function GenerateOverview(metamodel: Metamodel, databaseDefinitions: DatabaseDefinitions) : JSX.Element {
    return (
        <Accordion defaultActiveKey="0">
            { metamodel.databases != null
                ? metamodel.databases.map( (db, index) => CreateAccordionItem(metamodel, databaseDefinitions, db, index))
            : "" }
        </Accordion>
    )
}