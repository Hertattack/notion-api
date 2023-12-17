import {NodeRect} from "../components/Node.tsx";

export const defaultLayoutAlgorithm = (rects: {id: string, rect: NodeRect | null}[]) : {x: number, y: number, id: string}[] =>{
    const minHeight = 20;

    const minY = rects.reduce( (p,c)=> minOf(c.rect?.y, p) , 0);
    const minX = rects.reduce( (p,c)=> minOf(c.rect?.x, p) , 0);

    let nextHeight = minY;
    return rects.map( r=> {
        nextHeight += (r.rect?.height ?? minHeight) + 2;

        return {
            id: r.id,
            x: minX,
            y: nextHeight
       }
    });
}

function minOf(first: number | undefined, second: number){
    if(!first)
        return second;

    return first < second ? first : second;
}