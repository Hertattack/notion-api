import React, {useEffect, useRef} from "react";
import * as d3 from 'd3';

export interface SvgOptions {
    width: number,
    height: number,
    setup: (svgElement: SVGSVGElement, rootGroupElement: SVGSVGElement) => void
}

export const D3Svg : React.FC<SvgOptions> = (options)=>{
    const svgRef = useRef<SVGSVGElement|null>(null);
    const rootGroupRef = useRef<SVGSVGElement|null>(null);
    const {setup, width, height} = options;

    useEffect(()=>{
        if(svgRef.current === null || rootGroupRef.current === null)
            return;

        let rootGroupElement = rootGroupRef.current;
        let svgElement = svgRef.current;

        setup(svgElement, rootGroupElement);

        let d3Svg = d3.select(svgElement);
        let d3RootElement = d3.select(rootGroupElement);

        let zoomBehavior = d3.zoom<SVGSVGElement, unknown>()
            .scaleExtent([-500, 500])
            .translateExtent([
                [0, 0],
                [width, height],
            ])
            .on("zoom", (event)=> {
                d3RootElement.attr("transform", event.transform);
            });

        d3Svg.call(zoomBehavior);
    },[svgRef, rootGroupRef]);

    return (
        <svg ref={svgRef} width={width} height={height}>
            <g ref={rootGroupRef}></g>
        </svg>
    );
};