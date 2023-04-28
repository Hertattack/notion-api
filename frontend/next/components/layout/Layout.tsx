import React, {Fragment} from "react";
import {Container} from "react-bootstrap";
import {DefaultHeader} from "@/components/layout/DefaultHeader";

export type LayoutProps = {
    children: React.ReactNode
}

export const Layout = function({children}: LayoutProps) {
    return (
        <Fragment>
            <Container fluid={true}>
                <DefaultHeader/>
                <main>
                    {children}
                </main>
            </Container>
        </Fragment>
    )
}