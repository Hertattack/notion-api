import React from "react";
import {Accordion, ListGroup} from "react-bootstrap";

export const CheatSheet : React.FC = ()=>{
    return (
        <div className="cheat-sheet">
            <Accordion>
                <Accordion.Item eventKey="0">
                    <Accordion.Header>Query all rows in a single database</Accordion.Header>
                    <Accordion.Body>
                        <p>Use the alias of the table, and put it in parentheses. For example: <code>(tags)</code> where the database name is tags.</p>
                    </Accordion.Body>
                </Accordion.Item>
                <Accordion.Item eventKey="1">
                    <Accordion.Header>Filter a database</Accordion.Header>
                    <Accordion.Body>
                        <p>You can filter a database using the following format: <code>{"(models{Summary~='RICE'})"}</code> the example will filter the models table on the field with the name {"\"Summary\""} containing the string {"'RICE'"}.</p>
                        <p>The available operators are:</p>
                        <ul>
                            <li className="list-disc list-item"><code>{">"}</code> greater than</li>
                            <li className="list-disc list-item"><code>{">="}</code> greater or equal</li>
                            <li className="list-disc list-item"><code>{"<"}</code> less tha</li>
                            <li className="list-disc list-item"><code>{"<="}</code> less or equal</li>
                            <li className="list-disc list-item"><code>{"="}</code> equals</li>
                            <li className="list-disc list-item"><code>{"!"}</code> not</li>
                            <li className="list-disc list-item"><code>{"~="}</code> contains</li>
                            <li className="list-disc list-item"><code>{"?="}</code> starts with</li>
                            <li className="list-disc list-item"><code>{"=?"}</code> ends with</li>
                        </ul>
                    </Accordion.Body>
                </Accordion.Item>
                <Accordion.Item eventKey="2">
                    <Accordion.Header>Follow relations</Accordion.Header>
                    <Accordion.Body>
                        <p>The query language allows you to follow relations through roles. If you look at the metamodel you can see the roles that are available.</p>
                        <p>An example: <code>{"(articles)<-[ article | tag ]->(tags)"}</code> means that you can go from articles to tags using the role {"'tag'"}, and vice versa from tags to articles using the role {"'article'"}.</p>
                        <p>Going from articles to tags can be expressed as follows: <code>{"(articles)-[tag]->(tags)"}</code>.</p>
                        <p>The other way around: <code>{"(tags)-[article]->(articles)"}</code>.</p>
                        <p>And selecting a tag with the name {"'Notion'"} and then any related article, and then the tags belonging to the articles:<br/><code>{"(t:tags{Tag='Notion'})-[article]->(articles)-[tag]->(at:tags)"}</code>.</p>
                        <p>Note that the same database can be selected multiple times.</p>
                    </Accordion.Body>
                </Accordion.Item>
                <Accordion.Item eventKey="2">
                    <Accordion.Header>Aliasing and returning specific fields</Accordion.Header>
                    <Accordion.Body>
                        <p>To be documented. //TODO</p>
                    </Accordion.Body>
                </Accordion.Item>
                <Accordion.Item eventKey="100">
                    <Accordion.Header>Some example queries</Accordion.Header>
                    <Accordion.Body>
                        <ListGroup>
                            <ListGroup.Item><code>{"(models{Summary~='RICE'})-[tag]->(tags) RETURN tags.Tag, tags.Summary, models.Summary"}</code></ListGroup.Item>
                            <ListGroup.Item><code>{"(t:tags{Tag~='RICE'})-[article]->(articles)-[tag]->(at:tags) RETURN t.Tag, a.Name, at.Tag, at.Summary"}</code></ListGroup.Item>
                        </ListGroup>
                    </Accordion.Body>
                </Accordion.Item>
            </Accordion>
        </div>
    );
}