import React from "react";
import {Accordion} from "react-bootstrap";

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
            </Accordion>
        </div>
    );
}