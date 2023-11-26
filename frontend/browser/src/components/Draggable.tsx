import React, {useState} from "react";
import styled from "styled-components";

interface DraggableProps {
    id: string,
    children?: React.ReactNode
}

interface Position {
    x: number,
    y: number
}

export const Draggable : React.FC<DraggableProps> = ({id, children})=>{
    const [position, setPosition] = useState<Position>({x:0, y:0});
    const [dragging, setDragging] = useState<boolean>(false);
    const [relativePosition, setRelativePosition] = useState<Position|null>(null);

    const handleDragEvent = (event: React.DragEvent<HTMLDivElement>)=>{
        if(!dragging)
            return;

        const newX = event.pageX - (relativePosition?.x ?? 0);
        const newY = event.pageY - (relativePosition?.y ?? 0);

        setPosition({x: newX, y: newY});
    }

    const handleStartDragEvent = (event: React.DragEvent<HTMLDivElement>)=> {
        if(dragging)
            return;

        setDragging(true);
        const newRelativePosition = {
            x: event.pageX - event.currentTarget.offsetLeft,
            y: event.pageY - event.currentTarget.offsetTop
        };
        setRelativePosition(newRelativePosition);
    }

    const handleEndDragEvent = (event: React.DragEvent<HTMLDivElement>) => {
        if(!dragging)
            return;

        setDragging(false);

        const newX = event.pageX - (relativePosition?.x ?? 0);
        const newY = event.pageY - (relativePosition?.y ?? 0);
        setPosition({x: newX, y: newY});

        setRelativePosition(null);
    }

    return (
        <DraggableDiv id={id}
                      onDragStart={handleStartDragEvent}
                      onDragEnd={handleEndDragEvent}
                      onDrag={handleDragEvent} draggable={true} style={ position.y ? {top: position.y + 'px', left: position.x + 'px'} : {}}>
            {children}
        </DraggableDiv>
    )
};

const DraggableDiv = styled.div`
    position: absolute;
`;


//
// var Dragable = React.createClass({
//     getDefaultProps: function () {
//         return {
//             // allow the initial position to be passed in as a prop
//             initialPos: {x: 0, y: 0}
//         }
//     },
//     getInitialState: function () {
//         return {
//             pos: this.props.initialPos,
//             dragging: false,
//             rel: null // position relative to the cursor
//         }
//     },
//     // we could get away with not having this (and just having the listeners on
//     // our div), but then the experience would be possibly be janky. If there's
//     // anything w/ a higher z-index that gets in the way, then you're toast,
//     // etc.
//     componentDidUpdate: function (props, state) {
//         if (this.state.dragging && !state.dragging) {
//             document.addEventListener('mousemove', this.onMouseMove)
//             document.addEventListener('mouseup', this.onMouseUp)
//         } else if (!this.state.dragging && state.dragging) {
//             document.removeEventListener('mousemove', this.onMouseMove)
//             document.removeEventListener('mouseup', this.onMouseUp)
//         }
//     },
//
//     // calculate relative position to the mouse and set dragging=true
//     onMouseDown: function (e) {
//         // only left mouse button
//         if (e.button !== 0) return
//         var pos = $(this.getDOMNode()).offset()
//         this.setState({
//             dragging: true,
//             rel: {
//                 x: e.pageX - pos.left,
//                 y: e.pageY - pos.top
//             }
//         })
//         e.stopPropagation()
//         e.preventDefault()
//     },
//     onMouseUp: function (e) {
//         this.setState({dragging: false})
//         e.stopPropagation()
//         e.preventDefault()
//     },
//     onMouseMove: function (e) {
//         if (!this.state.dragging) return
//         this.setState({
//             pos: {
//                 x: e.pageX - this.state.rel.x,
//                 y: e.pageY - this.state.rel.y
//             }
//         })
//         e.stopPropagation()
//         e.preventDefault()
//     },
//     render: function () {
//         // transferPropsTo will merge style & other props passed into our
//         // component to also be on the child DIV.
//         return this.transferPropsTo(React.DOM.div({
//             onMouseDown: this.onMouseDown,
//             style: {
//                 position: 'absolute',
//                 left: this.state.pos.x + 'px',
//                 top: this.state.pos.y + 'px'
//             }
//         }, this.props.children))
//     }
// })