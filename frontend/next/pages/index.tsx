import {Col, Container, Nav, Navbar, NavDropdown, Row, Tab, Tabs} from "react-bootstrap";
import {FreeQuery} from "@/components/query/FreeQuery";
import {QueryHistory} from "@/components/query/QueryHistory";
import {MetamodelOverview} from "@/components/metamodel/MetamodelOverview";
import {CheatSheet} from "@/components/content/CheatSheet";

export default function Home() {
  return (
    <Container fluid={true}>
        <Navbar bg="light">
            <Container fluid={true}>
                <Navbar.Brand href="#home">Notion Explorer</Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav">
                    <Nav className="me-auto">
                        <Nav.Link href="#home">Home</Nav.Link>
                        <Nav.Link href="#link">Link</Nav.Link>
                        <NavDropdown title="Dropdown" id="basic-nav-dropdown">
                            <NavDropdown.Item href="#action/3.1">Action</NavDropdown.Item>
                            <NavDropdown.Item href="#action/3.2">
                                Another action
                            </NavDropdown.Item>
                            <NavDropdown.Item href="#action/3.3">Something</NavDropdown.Item>
                            <NavDropdown.Divider />
                            <NavDropdown.Item href="#action/3.4">
                                Separated link
                            </NavDropdown.Item>
                        </NavDropdown>
                    </Nav>
                </Navbar.Collapse>
            </Container>
        </Navbar>
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
                        <Tab title="Cheat Sheet" eventKey="cheat-sheet">
                            <CheatSheet/>
                        </Tab>
                    </Tabs>
                </Col>
            </Row>
        </Container>
    </Container>
  )
}
