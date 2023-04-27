import React from "react";
import { QueryInput } from "./QueryInput";
import {useAppSelector } from "@/store/hooks";
import { D3Graph } from "@/components/d3js";
import {Alert, Col, Row, Tab, Tabs} from "react-bootstrap";
import D3Node from "@/components/d3js/Node";
import {TableView} from "@/components/query/TableView";
import {QueryAnalysisVisualizer} from "@/components/query/analysis/QueryAnalysisVisualizer";

export const FreeQuery : React.FC = () => {
    const { nodes} = useAppSelector( state=>state.dataStore.data );
    const { loading, queryResult, error } = useAppSelector(state=>state.queryExecution);

    let d3NodeFunction = () : D3Node[] =>
        nodes.map( n => ({
            id: n.id,
            group: n.category
        }));

    function displayStatusBar() : JSX.Element {
        if(loading === 'failed') {
            return (
                <Row>
                    <Col>
                        <Alert variant="danger">
                        <Alert.Heading>Query has failed</Alert.Heading>
                            <p>Message: {error !== null ? error.message : "No error message given"}</p>
                            {error != null && error.data ? <div><hr/><pre><code>{error.data}</code></pre></div> : ""}
                        </Alert>
                    </Col>
                </Row>
            );
        }

        if(loading === 'idle'){
            return (
                <Row>
                    <Col>
                        <Alert variant="primary">Type a query to begin.</Alert>
                    </Col>
                </Row>
            )
        }

        return (
            <Row>
                <Col>
                    <Alert variant="primary">Query is being executed, please wait.</Alert>
                </Col>
            </Row>
        )
    }

    return (
    <div>
        <Row>
           <Col><QueryInput/></Col>
        </Row>
        { loading !== 'succeeded' ? displayStatusBar() : ""}
        <Row id="queryResult">
            <Col>
                <Tabs>
                    <Tab eventKey="tabular-data-view" title="Show data as table" className="table-data-view">
                        { queryResult !== null ? <TableView data={queryResult}/> : "No data"}
                    </Tab>
                    <Tab title="Query Plan" eventKey="query-plan-view" className="query-plan-view">
                        <QueryAnalysisVisualizer/>
                    </Tab>
                    <Tab eventKey="raw-data-view" title="Show raw response data" className="raw-data-view">
                        { queryResult !== null ? <pre><code>{JSON.stringify(queryResult,null, 2)}</code></pre> : "No data"}
                    </Tab>
                    <Tab title="Graph" eventKey="graph-view">
                        <D3Graph width={1000} height={1000} nodeFunction={d3NodeFunction}/>
                    </Tab>
                </Tabs>
            </Col>
        </Row>
    </div> )
};

