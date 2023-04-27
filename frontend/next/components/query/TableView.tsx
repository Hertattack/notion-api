import {Table} from "react-bootstrap";
import React from "react";
import FieldIdentifier from "@/notion-api/model/FieldIdentifier";
import Row from "@/notion-api/model/Row";
import FieldValueSet from "@/notion-api/model/FieldValueSet";
import QueryResult from "@/notion-api/model/QueryResult";

export interface TableViewProperties {
    data: QueryResult
}
export const TableView : React.FC<TableViewProperties> = (props: TableViewProperties)=>{
    const { data } = props;

    function CreateRowView(row : Row, index: number, fields: FieldIdentifier[]) : JSX.Element{
        function MapFieldInRow(field: FieldIdentifier) : JSX.Element{
            if(field.alias === null)
                throw new Error(`Somehow the field alias is null, that should not happen.`)

            let value = "";
            const valueSet : FieldValueSet = row.fieldValueSets[field.alias];
            if(valueSet !== undefined && valueSet.values[field.name] !== undefined)
                value =  valueSet.values[field.name];

            return (
                <td key={`${index}.${field.alias}${field.name}`}>{value}</td>
            )
        }

        return (
            <tr key={index}>
                <td>{index}</td>
                {fields.map(MapFieldInRow)}
            </tr>
        )
    }

    function CreatePropertyHeading(field : FieldIdentifier) : JSX.Element{
        const alias = field.alias === null ? "" : `${field.alias}.`;
        return (
            <th key={`${alias}${field.name}`}>{alias}{field.name}</th>
        )
    }

    return (
        <Table responsive={true} striped={true} bordered={true}>
            <thead>
                <tr>
                    <th>#</th>
                    {data.propertyNames.map(CreatePropertyHeading)}
                </tr>
            </thead>
            <tbody>
            {data.rows.map((r,i)=>CreateRowView(r, i, data.propertyNames))}
            </tbody>
        </Table>
    )
}