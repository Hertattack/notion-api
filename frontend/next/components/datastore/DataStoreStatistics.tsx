import React from "react";
import {useAppSelector} from "@/store/hooks";
import {Table} from "react-bootstrap";

export const DataStoreStatistics : React.FC = ()=>{
    const { data: dataStore} = useAppSelector(state=>state.dataStore);

    return (
        <div>
            <Table responsive={true} striped={true} bordered={true} key={`datastore-stats`}>
                <thead>
                    <tr>
                        <th>Category</th><th>Count</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Nodes</td><td>{dataStore.nodes.length}</td>
                    </tr>
                    <tr>
                        <td>Edges</td><td>{dataStore.edges.length}</td>
                    </tr>
                </tbody>
            </Table>
        </div>
    )
}