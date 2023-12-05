import React from "react";
import styled from "styled-components";
import {NodeSpecification} from "../NodeSpecification.ts";

interface NodeProps extends NodeSpecification {
}

export const Node : React.FC<NodeProps> = ({label})=>{
    return (
        <NodeDiv draggable={true}>
            {label}
        </NodeDiv>
    )
}

const NodeDiv = styled.div`
  border: 1px solid black;
  position: fixed;
  padding: 5px;
`;