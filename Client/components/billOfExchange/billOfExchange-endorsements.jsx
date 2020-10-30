import React from 'react';
import {Link, BrowserRouter}  from 'react-router-dom';

export default class Endorsements extends React.Component {
    render() {
        let data = this.props.data || [];
        let warningsArray = [].concat.apply([], data.map(item => item.warnings));

        return (
            <div className="inline-block">
                <h3>Endorsements</h3>
                <ul className="warnings">
                    {warningsArray.map(item => <li>{item}</li>)}
                </ul>
                <table className="table">
                    <tr className="header">
                        <td>Id</td>
                        <td>Id of previous</td>
                        <td>Beneficiary name</td>
                        <td>Bill Id</td>
                    </tr>
                    {data.map(item =>
                        (<tr>
                            <td>{item.id}</td>
                            <td>{item.previousEndorsementId}</td>
                            <td>
                                <Link to={`/parties/${item.newBeneficiaryId}`}>{item.newBeneficiaryName}</Link>
                            </td>
                            <td>{item.billId}</td>
                        </tr>))}
                </table>
            </div>
        );
    }
}
