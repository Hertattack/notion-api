import * as d3 from 'd3';

export default interface Node extends d3.SimulationNodeDatum {
    id: string,
    group: number
}