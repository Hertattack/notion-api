import {Col, Container, Row} from "react-bootstrap";
import {Canvas, CanvasPosition, CanvasRef} from "reaflow";
import {useAppSelector} from "@/store/hooks";
import {TransformComponent, TransformWrapper} from "react-zoom-pan-pinch";
import {Node as DataStoreNode} from "@/features/datastore/datastore-slice";
import React from "react";

export default function Index(data: any) : JSX.Element {
    const { data: dataStore } = useAppSelector(state => state.dataStore);
    let canvasRef = React.createRef<CanvasRef>();

    let usedNodeIds = new Set<number>();
    dataStore.edges.forEach( e=>{
        usedNodeIds.add(e.source);
        usedNodeIds.add(e.target);
    });

    let usedNodes : DataStoreNode[] = [];
    usedNodeIds.forEach( i=> usedNodes.push(dataStore.nodes[i]));

    const nodes = usedNodes.map( n => ({ id: n.id, text: n.label }));
    const edges = dataStore.edges.map( e=> ({ id: `${e.source}-${e.target}`, from: dataStore.nodes[e.source].id, to: dataStore.nodes[e.target].id, text: e.roles.join(", ")}));

    function onChange(data: any, ref: React.RefObject<CanvasRef>){
        if(ref.current === null)
            return;

        ref.current.canvasHeight = data.height;
        ref.current.canvasWidth = data.width;

        console.log(ref.current.canvasWidth);
        console.log(data);
    }

    return (
        <Container fluid={true}>
            <Row>
                <Col>
                    Nodes: {nodes.length}
                    Edges: {edges !== undefined ? edges.length : 0}
                </Col>
            </Row>
            <Row>
                <Col>
                    <TransformWrapper wheel={{ step: .2 }} maxScale={10} minScale={0.1} limitToBounds={false} centerOnInit={true} initialPositionX={0} initialPositionY={0}>
                        <TransformComponent>
                            <Canvas key="visualization"
                                    ref={canvasRef}
                                    zoomable={false}
                                    onLayoutChange={(data)=>onChange(data, canvasRef)}
                                    fit={true}
                                    defaultPosition={CanvasPosition.LEFT}
                                    nodes={nodes} edges={edges}/>
                        </TransformComponent>
                    </TransformWrapper>
                </Col>
            </Row>
        </Container>
    )
}