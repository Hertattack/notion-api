import {Col, Container, Nav, Navbar, NavDropdown, Row, Tab, Tabs} from "react-bootstrap";
import {FreeQuery} from "@/components/query/FreeQuery";
import {QueryHistory} from "@/components/query/QueryHistory";
import {MetamodelOverview} from "@/components/metamodel/MetamodelOverview";
import {CheatSheet} from "@/components/content/CheatSheet";
import React from "react";
import {DataStoreStatistics} from "@/components/datastore/DataStoreStatistics";

export default function Home() {
    return (
    <Container fluid={true}>
        <Row>
            <Col sm={8} className="with-border">
                <FreeQuery/>
            </Col>
            <Col className="with-border">
                <Tabs defaultActiveKey="metamodel">
                    <Tab title="Metamodel" eventKey="metamodel">
                        <div className="metamodel-overview-tab">
                            <MetamodelOverview/>
                        </div>
                    </Tab>
                    <Tab title="History" eventKey="queryHistory">
                        <div className="query-history-tab">
                            <QueryHistory/>
                        </div>
                    </Tab>
                    <Tab title="Data Store" eventKey="datastore">
                        <DataStoreStatistics/>
                    </Tab>
                    <Tab title="Cheat Sheet" eventKey="cheat-sheet">
                        <CheatSheet/>
                    </Tab>
                </Tabs>
            </Col>
        </Row>
    </Container>
  )
}
