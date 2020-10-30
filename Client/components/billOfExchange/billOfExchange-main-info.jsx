import React from 'react';
import {Link, BrowserRouter}  from 'react-router-dom';

export default class MainInfo extends React.Component {
    render() {
        let data = this.props.data || [];
        let warningsArray = data.warnings;

        return (
            <div className="inline-block">
                <h3>Main info</h3>
                <ul className="warnings">
                    {warningsArray.map(item => <li>{item}</li>)}
                </ul>
                <table>
                    <tr>
                        <td className="header">Drawer: </td>
                        <td>
                            <Link to={`/parties/${data.drawerId}`}>{data.drawerName}</Link>
                        </td>
                    </tr>
                    <tr>
                        <td className="header">First beneficiary: </td>
                        <td>
                            <Link to={`/parties/${data.beneficiaryId}`}>{data.beneficiaryName}</Link>
                        </td>
                    </tr>
                    <tr>
                        <td className="header">Current beneficiary: </td>
                        <td>
                            <Link to={`/parties/${data.currentBeneficiaryId}`}>{data.currentBeneficiaryName}</Link>
                        </td>
                    </tr>
                    <tr>
                        <td className="header">Amount: </td>
                        <td>{data.amount}</td>
                    </tr>
                </table>
            </div>
        );
    }
}
