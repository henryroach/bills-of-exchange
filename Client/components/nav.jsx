import React from 'react';
import { NavLink, BrowserRouter } from 'react-router-dom';

export default class Nav extends React.Component {
    render() {
        return <ul class="navbar">
            <li><NavLink exact to="/" activeClassName="active">Main page</NavLink></li>
            <li><NavLink to="/parties" activeClassName="active">Parties</NavLink></li>
            <li><NavLink to="/billOfExchanges" activeClassName="active">Bills of exchanges</NavLink></li>
        </ul>

    }
}