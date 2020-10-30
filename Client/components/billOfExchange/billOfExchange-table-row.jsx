import React from 'react';
import {Link, BrowserRouter}  from 'react-router-dom';

export default class BillOfExchangeTR extends React.Component{
    render(){
        return <tr>
                <td>{this.props.data.id}</td>
                <td>{this.props.data.drawerName}</td>
                <td>{this.props.data.beneficiaryName}</td>
                <td>{this.props.data.amount}</td>
                <td className="pull-right">
                    <Link to={`/billOfExchanges/${this.props.data.id}`}>Details</Link>
                </td>
            </tr>;
    }
}
