import {Container, Nav, Navbar, NavDropdown} from "react-bootstrap";
import React from "react";
import Link from "next/link";

export const DefaultHeader : React.FC = ()=>{

    return (
        <Navbar bg="light">
            <Container fluid={true}>
                <Navbar.Brand><Link href="/" className="inherit-link-style">Notion Explorer</Link></Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav">
                    <Nav className="me-auto">
                        <Nav.Link><Link href="/" className="inherit-link-style">Home</Link></Nav.Link>
                        <Nav.Link><Link href="/visualization/" className="inherit-link-style">Visualization</Link></Nav.Link>
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
    )
}