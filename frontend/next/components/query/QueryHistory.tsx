import React from "react";
import {useAppDispatch, useAppSelector} from "@/store/hooks";
import {Button, ListGroup, ListGroupItem} from "react-bootstrap";
import {
    clearQueryHistory,
    deleteSelectedQueryFromHistory,
    selectQueryFromHistory
} from "@/features/querying/queryHistory-slice";

export const QueryHistory : React.FC = ()=> {
    const dispatch = useAppDispatch();
    const { previousQueries, selectedIndex } = useAppSelector(state=>state.queryHistory);

    function changeSelectedIndex(index : number){
        if(index != selectedIndex)
            dispatch(selectQueryFromHistory(index));
    }

    function clearHistory(){
        dispatch(clearQueryHistory())
    }

    function deleteSelectedItem(){
        dispatch(deleteSelectedQueryFromHistory())
    }

    return (
        <div>
            <Button onClick={deleteSelectedItem}>Delete Selected</Button><Button onClick={clearHistory}>Clear History</Button>
            {previousQueries.length > 0 ?
                <div>
                    <ListGroup>
                        {previousQueries.map((pq, i)=>(
                            <ListGroupItem onClick={()=>changeSelectedIndex(i)} active={i==selectedIndex} key={`${i}.${pq}`}>{pq}</ListGroupItem>
                        ))}
                    </ListGroup>
                </div>
                :""
            }
        </div>
    )
}