import React from "react";
import {useAppSelector} from "@/store/hooks";
import {ListGroup, ListGroupItem} from "react-bootstrap";

export const QueryHistory : React.FC = ()=> {
    const { previousQueries, selectedIndex } = useAppSelector(state=>state.queryHistory);

    return (
        <div>
            {previousQueries.length > 0 ?
                <div>
                    <ListGroup>
                        {previousQueries.map((pq, i)=>(
                            <ListGroupItem key={`${i}.${pq}`}>{pq}</ListGroupItem>
                        ))}
                    </ListGroup>
                    <ol>
                        {previousQueries.map((pq, i)=>(
                            <li key={`${i}.${pq}`}>{pq}</li>
                        ))}
                    </ol>
                </div>
                :""
            }
        </div>
    )
}