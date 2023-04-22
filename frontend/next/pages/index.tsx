import { Col, Container, Row } from "react-bootstrap";
import {FreeQuery} from "@/components/query/FreeQuery";

export default function Home() {
  return (
    <Container className="root">
        <Row>
            <Col sm={9} className="with-border">
                <FreeQuery/>
            </Col>
            <Col className="with-border">
                <p>This should be a sidebar</p>
            </Col>
        </Row>
    </Container>
  )
}
