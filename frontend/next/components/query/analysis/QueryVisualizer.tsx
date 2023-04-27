import React from "react";
import {useAppSelector} from "@/store/hooks";
import {Col, Row} from "react-bootstrap";
import Metamodel from "@/notion-api/model/metadata/Metamodel";
import QuerySpecification from "@/notion-api/model/analysis/QuerySpecification";
import QueryDatabaseReference from "@/notion-api/model/analysis/QueryDatabaseReference";
import {Table} from "react-bootstrap";
import OutputPropertySelection from "@/notion-api/model/analysis/OutputPropertySelection";
import QuerySelectStep from "@/notion-api/model/analysis/QuerySelectStep";
import QueryFilterExpression from "@/notion-api/model/analysis/QueryFilterExpression";
import {emptyModel} from "@/features/metamodel/metamodel-slice";

export const QueryVisualizer : React.FC<{query:QuerySpecification}> = ({query})=>{
    const { metamodel } = useAppSelector(state => state.metamodel);

    return (
        <div className="query-visualization">
            {displayTablesUsed(query.databaseReferences, metamodel ?? emptyModel)}
            {displayOutputSelection(query.allPropertiesSelected, query.propertiesSelected, metamodel ?? emptyModel)}
            {displaySelectionSteps(query.selectSteps, metamodel ?? emptyModel)}
        </div>
    )
}

function displaySelectionSteps(selectSteps: QuerySelectStep[], metamodel: Metamodel) : JSX.Element {
    return(
        <div>
            <h5>Selection steps / joins</h5>
            <Table className="query-visualization-select-steps" responsive={true} striped={true} bordered={true}>
                <thead>
                <tr>
                    <th>#</th><th>Alias</th><th>Query Alias</th><th>Filters</th>
                </tr>
                </thead>
                <tbody>
                { selectSteps.map( (s,i)=>{
                    var reference = s.databaseReference;
                    return (
                        <tr key={`qstep-${reference.databaseAlias}-${reference.queryAlias}-${i}`}>
                            <td>{i}</td><td>{reference.databaseAlias}</td><td>{reference.queryAlias}</td><td>{s.filters.length == 0 ? "No filters" : mapFilters(s.filters)}</td>
                        </tr>
                    )
                })}
                </tbody>
            </Table>
        </div>
    )
}

function mapFilters(filters: QueryFilterExpression[]) : JSX.Element {
    return (
        <ul>
            {filters.map( (f,i)=> <li key={i} className="list-item list-decimal">{f.description}</li>)}
        </ul>
    );
}

function displayOutputSelection(allPropertiesSelected: QueryDatabaseReference[], propertiesSelected: OutputPropertySelection[], metamodel: Metamodel): JSX.Element {
    return (
        <div>
            <h5>Properties selected for output</h5>
            <Table className="query-visualization-output-selection" responsive={true} striped={true} bordered={true}>
                <thead>
                <tr>
                    <th>Alias</th><th>Query Alias</th><th>Property</th>
                </tr>
                </thead>
                <tbody>
                {allPropertiesSelected.map((a,i)=>{
                    return (
                        <tr key={`qoa-${a.databaseAlias}-${a.queryAlias}-${i}`}>
                            <td>{a.databaseAlias}</td><td>{a.queryAlias}</td><td><b>*</b></td>
                        </tr>
                    )
                })}
                {propertiesSelected.map( (p,i) => {
                    var reference = p.databaseReference;
                    return p.propertyNames.map( (n,ni) => {
                        return (
                            <tr key={`qop-${reference.databaseAlias}-${reference.queryAlias}-${i}-${n}-${ni}`}>
                                <td>{reference.databaseAlias}</td><td>{reference.queryAlias}</td><td>{n}</td>
                            </tr>
                        )
                    })
                })}
                </tbody>
            </Table>
        </div>
    )
}

function displayTablesUsed(references : QueryDatabaseReference[], metamodel : Metamodel) : JSX.Element {
    return (
        <div>
            <h5>Databases Referenced</h5>
            <Table className="query-visualization-database-references" responsive={true} striped={true} bordered={true}>
                <thead>
                    <tr>
                        <th>Database</th><th>Alias</th><th>Query Alias</th>
                    </tr>
                </thead>
                <tbody>
                {references.map( (r,i) => {
                    var db = metamodel.databases.find( d=>d.alias == r.databaseAlias);
                    return (
                        <tr key={`qdbr-${r.databaseAlias}-${r.queryAlias}-${i}`}>
                            <td>{db !== undefined ? db.id : "?"}</td><td>{r.databaseAlias}</td><td>{r.queryAlias}</td>
                        </tr>
                    ) })}
                </tbody>
            </Table>
        </div>
    )
}