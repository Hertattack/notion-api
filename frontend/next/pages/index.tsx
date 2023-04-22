import {Badge, Tab, TabContainer, Tabs} from "react-bootstrap";

export default function Home() {
  return (
    <div>
      <h1>Hello World</h1>
        <Badge bg="secondary">
            New
        </Badge>
        <TabContainer>
            <Tabs defaultActiveKey={"1"}>
                <Tab eventKey={"1"} title={"One"}>
                    <h2>Hi</h2>
                    <p>moar</p>
                </Tab>
                <Tab eventKey={"2"} title={"Two"}>
                    <h2>Two</h2>
                    <p>moar</p>
                </Tab>
            </Tabs>
        </TabContainer>
    </div>
  )
}
