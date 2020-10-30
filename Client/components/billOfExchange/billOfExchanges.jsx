import React from 'react';
import ListPageComponent from '../list-page-component.jsx';
import BillOfExchangeTR from './billOfExchange-table-row.jsx';

export default class BillOfExchanges extends ListPageComponent {
    constructor(props) {
        super(props, "Bills of exchanges", "/BillOfExchange/GetItems");
    }

    componentDidMount() {
        super.loadData();
    }

    render() {
        let data = this.state.data || [];
        let warningsArray = [].concat.apply([], data.map(item => item.warnings));

        return super.renderPage(
            warningsArray,
            <table className="table">
                <tbody>
                    <tr className="header">
                        <td>Id</td>
                        <td>Drawer name</td>
                        <td>Beneficiary name</td>
                        <td>Amount</td>
                        <td></td>
                    </tr>
                    {data.map(item => (
                        <BillOfExchangeTR data={item} />
                    ))}
                </tbody>
            </table>
        );
    }
}
