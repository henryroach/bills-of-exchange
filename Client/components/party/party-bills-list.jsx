import React from 'react';
import { Link, BrowserRouter } from 'react-router-dom';

export default class BillsList extends React.Component {
    render() {
        let header = this.props.header || "";
        let data = this.props.data || [];
        let warningsArray = [].concat.apply([], data.map(item => item.warnings));

        if (data.length == 0 && warningsArray.length == 0)
        {
            return <ul className="warnings">
                {warningsArray.map(item => <li>{item}</li>)}
            </ul>;
        }

        return (
            <div className="inline-block">
                <h3>{header}</h3>
                <ul className="warnings">
                    {warningsArray.map(item => <li>{item}</li>)}
                </ul>
                <table className="table">
                    <tr className="header">
                        <td>Id</td>
                        <td>Drawer id</td>
                        <td>Drawer name</td>
                        <td>Beneficiary id</td>
                        <td>Beneficiary name</td>
                        <td>Amount</td>
                    </tr>
                    {data.map(item =>
                        (<tr>
                            <td>{item.id}</td>
                            <td>{item.drawerId}</td>
                            <td>{item.drawerName}</td>
                            <td>{item.beneficiaryId}</td>
                            <td>{item.beneficiaryName}</td>
                            <td>{item.amount}</td>
                            <td>
                                <Link to={`/billOfExchanges/${item.id}`}>Details</Link>
                            </td>
                        </tr>))}
                </table>
            </div>
        );

    }
}
