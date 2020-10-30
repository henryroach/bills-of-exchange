import React from 'react';
import MainInfo from './billOfExchange-main-info.jsx';
import Endorsements from './billOfExchange-endorsements.jsx';

export default class BillOfExchange extends React.Component {
    constructor(props) {
        super(props);

        appStorage.routing.setLastRoute(props.history.location.pathname);

        let id = this.props.match.params.id;
        
        let urlMain = appStorage.api.getFullUrl(`/BillOfExchange/GetById/${id}`);
        let urlEndorsements = appStorage.api.getFullUrl(`/BillOfExchange/GetEndorsementsByBillId/${id}`);

        this.state = {
            header: "Bills of exchanges. Detail info",
            urlMain: urlMain,
            errorMain: null,
            isLoadedMain: false,
            dataMain: null,
            urlEndorsements: urlEndorsements,
            errorEndorsements: null,
            isLoadedEndorsements: false,
            dataEndorsements: null,
        };
    }

    render() {
        const { errorMain, isLoadedMain, dataMain } = this.state;
        const { errorEndorsements, isLoadedEndorsements, dataEndorsements } = this.state;
        const header = <h2>{this.state.header}</h2>;

        let main, endorsements;

        if (errorMain) { main = <div>{error}</div> }
        else if (!isLoadedMain) { main = <div>Loading...</div> }
        else { main = <MainInfo data={dataMain}></MainInfo> }

        if (errorEndorsements) { endorsements = <div>{error}</div> }
        else if (!isLoadedEndorsements) { endorsements = <div>Loading...</div> }
        else { endorsements = <Endorsements data={dataEndorsements}></Endorsements> }

        return (
            <div>
                {header}
                <div>
                {main}
                {endorsements}
                </div>
            </div>
        );
    }

    componentDidMount() {
        this.dataLoader(this.state.urlMain, "Main");
        this.dataLoader(this.state.urlEndorsements, "Endorsements");
    }

    dataLoader(url, statesSuffix) {
        var url = this.state["url" + statesSuffix]

        fetch(url).then((response) => {
            return response.json();
        }).then(
            (result) => {
                // Data was loaded successfully
                if (result.errorcode === undefined) {
                    let updateObj = {};
                    updateObj["isLoaded" + statesSuffix] = true;
                    updateObj["data" + statesSuffix] = result;
                    updateObj["error" + statesSuffix] = null;
                    this.setState(updateObj);
                }
            },
            (error) => {
                let errorMessage = "Can't load the data: connection is failed";
                console.error(errorMessage);

                let updateObj = {};
                updateObj["isLoaded" + statesSuffix] = true;
                updateObj["data" + statesSuffix] = null;
                updateObj["error" + statesSuffix] = errorMessage;
                this.setState(updateObj);
            }
        )
    }

}