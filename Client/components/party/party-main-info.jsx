import React from 'react';

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
                        <td className="header">Name: </td>
                        <td>{data.name}</td>
                    </tr>
                </table>
            </div>
        );
    }
}
