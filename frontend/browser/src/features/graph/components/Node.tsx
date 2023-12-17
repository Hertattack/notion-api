import React, {useState} from "react";
import styled from "styled-components";

interface NodeProps {
    children?: React.ReactNode
}

export const Node : React.FC<NodeProps> = ({children})=>{
    const [dragging, setDragging] = useState(false);
    const [position, setPosition] = useState({x:0,y:0})
    const [offset, setOffset] = useState({left: 0, top: 0});

    const handleDragStart =  (event: React.DragEvent<HTMLDivElement>)=>{
        setDragging(true);

        const offsetLeft = event.currentTarget.offsetLeft - event.clientX;
        const offsetTop = event.currentTarget.offsetTop - event.clientY;

        setOffset({left: offsetLeft, top: offsetTop});
    };

    const handleDragEnd = (event: React.DragEvent<HTMLDivElement>) => {
        if(!dragging)
            return;

        setDragging(false);
        setPosition({ x: event.clientX + offset.left, y: event.clientY + offset.top });
        setOffset({left:0, top:0});
    }

    return (
        <NodeDiv draggable
                 onDragStart={handleDragStart}
                 onDragEnd={handleDragEnd}
                 style={{left: position.x, top: position.y}}>
            {children}
        </NodeDiv>
    )
}

const NodeDiv = styled.div`
  border: 1px solid black;
  position: fixed;
  padding: 5px;
`;