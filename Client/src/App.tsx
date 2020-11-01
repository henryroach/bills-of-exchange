import React, { FC } from 'react'
import { BrowserRouter as Router, Switch, Route, Link } from 'react-router-dom'
import { Parties } from './Parties'
import { BillsOfExchange } from './BillsOfExchange'
import { Party } from './Party'

export const App: FC = () => {
    return (
        <Router>
            <div>
                <Link to="/">Home</Link> | <Link to="/parties">Parties</Link> | <Link to="/billsOfExchange">Bills of exchange</Link>
            </div>

            <Switch>
                <Route path="/parties" component={Parties} />
                <Route path="/party/:id" component={Party} />
                <Route path="/billsOfExchange" component={BillsOfExchange} />
            </Switch>
        </Router>
    )
}