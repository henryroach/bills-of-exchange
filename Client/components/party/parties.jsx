import React from 'react';
import ListPageComponent from '../list-page-component.jsx';
import PartyTR from './party-table-row.jsx';

export default class Parties extends ListPageComponent {
    constructor(props) {
        super(props, "Parties", "/party/GetItems");
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
                        <td>Name</td>
                        <td></td>
                    </tr>
                    {data.map(item => (
                        <PartyTR data={item} />
                    ))}
                </tbody>
            </table>
        );
    }
}
