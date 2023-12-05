import React, {createRef, useLayoutEffect} from "react";
import {Node} from "./components/Node.tsx";
import {NodeSpecification} from "./NodeSpecification.ts";
import {defaultLayoutAlgorithm} from "./lib/basicLayoutAlgorithm.ts";


interface GraphProps {
    id: string;
    nodes: NodeSpecification[];
}

export const Graph : React.FC<GraphProps> = ({id, nodes})=>{
    const graphRefs : { [id: string]: React.RefObject<HTMLDivElement> } = {};

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
                const ref = createRef<HTMLDivElement>();
                graphRefs[n.id] = ref;
                return (
                    <div ref={ref} key={n.id}>
                        <Node label={n.label} id={n.id}/>
                    </div>
                );
            })}
        </div>
    );
}

function getClientRects(graphRefs : { [id: string]: React.RefObject<HTMLDivElement> }) : { id: string, rect: DOMRect }[] {
    return Object.getOwnPropertyNames(graphRefs)
        .filter( id => graphRefs[id].current !== null)
        .map( id => {
            const ref = graphRefs[id];

            return { rect: ref.current!.getBoundingClientRect(), id};
        });
}