import React from "react";
import {D3Svg} from "./lib/D3Svg";
import * as d3 from "d3";
import Node from './Node';

export interface GraphOptions {
    width: number,
    height: number,
    nodes?: Node[],
    nodeFunction?: () => Node[]
}

export const D3Graph : React.FC<GraphOptions> = (options)=>{
    const {width, height} = options;

    return (
       <D3Svg width={width} height={height} setup={(svgElement, rootGroupElement)=>{setup(svgElement, rootGroupElement, options);}}/>
    );
}


function setup(svgElement: SVGSVGElement, rootGroupElement: SVGSVGElement, options: GraphOptions){
    const {nodes, nodeFunction , width, height} = options;
    const calculatedNodes = nodeFunction !== undefined ? nodeFunction() : (nodes !== undefined ? nodes : []);

    d3.forceSimulation(calculatedNodes)
        .force('charge', d3.forceManyBody())
        .force('center', d3.forceCenter(width / 2, height / 2))
        .on('tick', () => { ticked(svgElement, calculatedNodes); });

}

function ticked(current: SVGSVGElement, nodes: Node[]) {
    d3.select(current)
        .selectAll('circle')
        .data(nodes)
        .join('circle')
        .attr('r', 5)
        .attr('cx', d => d.x ?? 0)
        .attr('cy', d => d.y ?? 0);
}