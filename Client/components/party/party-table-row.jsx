import React from 'react';
import {Link, BrowserRouter}  from 'react-router-dom';

export default class PartyTR extends React.Component{
    render(){
        return <tr>
                <td>{this.props.data.id}</td>
                <td>{this.props.data.name}</td>
                <td className="pull-right">
                    <Link to={`/parties/${this.props.data.id}`}>Details</Link>
                </td>
            </tr>;
    }
}
