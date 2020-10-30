import React from 'react';
import { Redirect } from 'react-router-dom'

export default class Home extends React.Component {
    constructor(props) {
        super(props);

        this.state = {};

        var isFirstLoad = appStorage.routing.isFirstLoad;
        var lastRoutePath = appStorage.routing.getLastRoute();
        
        if (isFirstLoad && lastRoutePath) {
            this.state.isFirstLoad = isFirstLoad;
            this.state.lastRoutePath = lastRoutePath;
        }
        else {
            appStorage.routing.clearLastRoute();
        }
    }

    render() {
        if (this.state.isFirstLoad) {
            return <Redirect to={this.state.lastRoutePath} />
        }

        return <h2>Main page</h2>;
    }
}