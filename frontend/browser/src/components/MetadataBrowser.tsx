import React from "react";
import {useStore} from "../store/StoreContext.tsx";
import {observer} from "mobx-react-lite";
import {DraggableBox} from "./DraggableBox.tsx";
import Xarrow, {Xwrapper} from "react-xarrows";

interface MetadataBrowserProps {
    children? : React.ReactNode;
}

export const MetadataBrowser: React.FC<MetadataBrowserProps> = observer(() => {
    const { notionStore} = useStore();

    notionStore.metadataStore.initialize()

    const metamodel = notionStore.metadataStore.metamodel;

    return (
        <>
            <Xwrapper>
                <div>
                    {metamodel.databases.map(
                        d=> (<DraggableBox key={d.id} id={d.id}>{d.alias}</DraggableBox>)
                    )}
                </div>
                {metamodel.edges.map( e => {
                    const fromDb = metamodel.databases.find(d=>d.alias == e.from.alias);
                    const toDb = metamodel.databases.find(d=>d.alias == e.to.alias);

                    if(!fromDb ||  !toDb)
                        return (<></>);

                    return (
                        <>
                            { e.navigability.forward ? <Xarrow start={fromDb.id} end={toDb.id} strokeWidth={1} labels={e.navigability.forward.label}/> : <></> }
                            { e.navigability.reverse ? <Xarrow start={toDb.id} end={fromDb.id} strokeWidth={1} labels={e.navigability.reverse.label}/> : <></> }
                        </>
                    );
                })}
            </Xwrapper>
        </>
    );
});