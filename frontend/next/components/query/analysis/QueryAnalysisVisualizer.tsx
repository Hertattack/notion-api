import React from "react";
import {useAppSelector} from "@/store/hooks";
import {Alert, Col, Row} from "react-bootstrap";
import FailedQueryAnalysisResult from "@/notion-api/status/FailedQueryAnalysisResult";
import {QueryVisualizer} from "@/components/query/analysis/QueryVisualizer";

export const QueryAnalysisVisualizer : React.FC = ()=> {
    const { loading, error, analysisResult} = useAppSelector(state=>   state.queryAnalysis);

    return (
        <div>
            {showStatus(loading, error)}
            {analysisResult != null ?
                <div>
                    <h4>Exection Plan</h4>
                    <ol>
                        { analysisResult.planSteps.map( s=><li className="list-item list-decimal" key={s.order}>{s.description}</li>)}
                    </ol>
                    <h4>Query Details</h4>
                    <hr/>
                    <QueryVisualizer query={analysisResult.querySpecification}/>
                </div>
                : ""}
        </div>
    )
}

function showStatus(loading : string, error : FailedQueryAnalysisResult | null) : JSX.Element {
    if(loading == "pending") {
        return (
            <Row>
                <Col><Alert variant="primary">Waiting for analysis result</Alert></Col>
            </Row>
        )
    }

    if(loading == "failed"){
        return (
            <Row>
                <Col>
                    <Alert variant="danger">
                        <Alert.Heading>Query analysis has failed.</Alert.Heading>
                        <p>Message: {error !== null ? error.message : "No error message given"}</p>
                        {error != null && error.data ? <div><hr/><pre><code>{error.data}</code></pre></div> : ""}
                    </Alert>
                </Col>
            </Row>
        )
    }

    return (
        <></>
    )
}

