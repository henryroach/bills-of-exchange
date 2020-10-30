import ReactDOM from 'react-dom';
import React from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import Nav from './components/nav.jsx';
import Home from './components/home.jsx';
import Parties from './components/party/parties.jsx';
import Party from './components/party/party.jsx';
import BillOfExchanges from './components/billOfExchange/billOfExchanges.jsx';
import BillOfExchange from './components/billOfExchange/billOfExchange.jsx';
import NotFound from './components/notfound.jsx';

class PartiesRoute extends React.Component{
    render(){
        return <Switch>
                    <Route exact path="/parties" component={Parties} />
                    <Route exact path="/parties/:id(\d+)" component={Party} />
                    <Route component={NotFound} />
                </Switch>;
    }
}

class BillOfExchangesRoute extends React.Component{
    render(){
        return <Switch>
                    <Route exact path="/billOfExchanges" component={BillOfExchanges} />
                    <Route exact path="/billOfExchanges/:id(\d+)" component={BillOfExchange} />
                    <Route component={NotFound} />
                </Switch>;
    }
}

ReactDOM.render(
    <Router>
        <div>
           <Nav />
            <Switch>
                <Route exact path="/" component={Home} />
                <Route path="/parties" component={PartiesRoute} />
                <Route path="/billOfExchanges" component={BillOfExchangesRoute} />
                <Route component={NotFound} />
            </Switch>
        </div>
    </Router>,
    document.getElementById("app")
)