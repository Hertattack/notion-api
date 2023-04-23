import { Col, Container, Row } from "react-bootstrap";
import {FreeQuery} from "@/components/query/FreeQuery";
import {QueryHistory} from "@/components/query/QueryHistory";

export default function Home() {
  return (
    <Container className="root">
        <Row>
            <Col sm={9} className="with-border">
                <FreeQuery/>
            </Col>
            <Col className="with-border">
                <QueryHistory/>
            </Col>
        </Row>
    </Container>
  )
}
