import React, {useImperativeHandle, useRef, useState} from "react";
import styled from "styled-components";

interface NodeProps {
    apiRef: React.RefObject<NodeApi>,
    x?: number,
    y?: number,
    children?: React.ReactNode
}

export type NodeRect = {
    x:number; y:number, width:number, height:number
}

export type NodeApi = {
    getBoundingClientRect: () => null | NodeRect;
};

export const Node : React.FC<NodeProps> = ({apiRef, x, y, children})=>{
    const internalRef = useRef<HTMLDivElement>(null);
    const [dragging, setDragging] = useState(false);
    const [position, setPosition] = useState({x:x??0,y:y??0});
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

    useImperativeHandle(
        apiRef,
        ()=> ({
            getBoundingClientRect: () => {
                if(!internalRef.current)
                    return null;

                const rect = internalRef.current.getBoundingClientRect();
                return {
                    x: rect.x,
                    y: rect.y,
                    width: rect.width,
                    height: rect.height
                };
            }
        }),
        []
    );

    return (
        <NodeDiv draggable
                 ref={internalRef}
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