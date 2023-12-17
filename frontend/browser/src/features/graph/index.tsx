import React, {createRef, useLayoutEffect} from "react";
import {Node, NodeApi, NodeRect} from "./components/Node.tsx";
import {NodeSpecification} from "./NodeSpecification.ts";
import {defaultLayoutAlgorithm} from "./lib/basicLayoutAlgorithm.ts";


interface GraphProps {
    id: string;
    nodes: NodeSpecification[];
}

export const Graph : React.FC<GraphProps> = ({id, nodes})=>{
    const graphRefs : { [id: string]: React.RefObject<NodeApi> } = {};

    useLayoutEffect(() => {
        const rects = getClientRects(graphRefs);
        if(rects.length > 0){
           const newRects = defaultLayoutAlgorithm(rects);
           console.log(newRects);
        }

    }, [graphRefs]);

    return (
        <div id={id}>
            {nodes.map(n=> {
                const ref = createRef<NodeApi>();
                graphRefs[n.id] = ref;
                return (
                    <Node apiRef={ref} key={n.id}>{n.label}</Node>
                );
            })}
        </div>
    );
}

function getClientRects(graphRefs : { [id: string]: React.RefObject<NodeApi> }) : { id: string, rect: NodeRect | null }[] {
    return Object.getOwnPropertyNames(graphRefs)
        .filter( id => graphRefs[id].current !== null)
        .map( id => {
            const ref = graphRefs[id];
            return { rect: ref.current!.getBoundingClientRect(), id};
        });
}