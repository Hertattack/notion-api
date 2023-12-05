export const defaultLayoutAlgorithm = (rects: {id: string, rect: DOMRect}[]) : {x: number, y: number, id: string}[] =>{
    const minY = rects.reduce( (p,c)=> p > c.rect.y ? c.rect.y : p , 0);
    const minX = rects.reduce( (p,c)=> p > c.rect.x ? c.rect.x : p , 0);

    let nextHeight = minY;
    return rects.map( r=> {
        const y = nextHeight;
        nextHeight += r.rect.height + 2;

        return {
            id: r.id,
            x: minX,
            y
       }
    });
}