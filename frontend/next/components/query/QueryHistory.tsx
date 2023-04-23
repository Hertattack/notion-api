import React from "react";
import {useAppSelector} from "@/store/hooks";

export const QueryHistory : React.FC = ()=> {
    const { previousQueries } = useAppSelector(state=>state.queryHistory);

    return (
        <div>
            {previousQueries.length > 0 ?
                <div>
                    <ol>
                        {previousQueries.map((pq)=>(
                            <li key={pq}>{pq}</li>
                        ))}
                    </ol>
                </div>
                :""
            }
        </div>
    )
}