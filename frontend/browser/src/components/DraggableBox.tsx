import React from 'react';
import {useXarrow} from 'react-xarrows';
import Draggable from 'react-draggable';
import styled from "styled-components";

interface DraggableBoxProps {
    id: string
}

export const DraggableBox : React.FC<DraggableBoxProps> = ({id}) => {
    const updateXarrow = useXarrow();
    return (
        <Draggable onDrag={updateXarrow} onStop={updateXarrow}>
            <Box id={id}>
                {id}
            </Box>
        </Draggable>
    );
};

const Box = styled.div `
  border: grey solid 2px;
  border-radius: 10px; 
  padding: 5px;
`;