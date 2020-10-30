import React from 'react';
import MainInfo from './party-main-info.jsx';
import BillsList from './party-bills-list.jsx';

export default class Party extends React.Component {
    constructor(props) {
        super(props);

        appStorage.routing.setLastRoute(props.history.location.pathname);

        let id = this.props.match.params.id;
        
        let urlMain = appStorage.api.getFullUrl(`/Party/GetById/${id}`);
        let urlDrawers = appStorage.api.getFullUrl(`/Party/GetBillsByDrawerId/${id}`);
        let urlBeneficiaries = appStorage.api.getFullUrl(`/Party/GetBillsByBeneficiaryId/${id}`);

        this.state = {
            header: "Party. Detail info",
            urlMain: urlMain,
            errorMain: null,
            isLoadedMain: false,
            dataMain: null,
            
            urlDrawers: urlDrawers,
            errorDrawers: null,
            isLoadedDrawers: false,
            dataDrawers: null,
            
            urlBeneficiaries: urlBeneficiaries,
            errorBeneficiaries: null,
            isLoadedBeneficiaries: false,
            dataBeneficiaries: null,
        };
    }

    render() {
        const { errorMain, isLoadedMain, dataMain } = this.state;
        const { errorDrawers, isLoadedDrawers, dataDrawers } = this.state;
        const { errorBeneficiaries, isLoadedBeneficiaries, dataBeneficiaries } = this.state;
        const header = <h2>{this.state.header}</h2>;

        let main, drawers, beneficiaries;

        if (errorMain) { main = <div>{error}</div> }
        else if (!isLoadedMain) { main = <div>Loading...</div> }
        else { main = <MainInfo data={dataMain}></MainInfo> }

        if (errorDrawers) { drawers = <div>{error}</div> }
        else if (!isLoadedDrawers) { drawers = <div>Loading...</div> }
        else { drawers = <BillsList data={dataDrawers} header="Drawer bills"></BillsList> }

        if (errorBeneficiaries) { beneficiaries = <div>{error}</div> }
        else if (!isLoadedBeneficiaries) { beneficiaries = <div>Loading...</div> }
        else { beneficiaries = <BillsList data={dataBeneficiaries} header="Beneficiary bills"></BillsList> }

        return (
            <div>
                {header}
                {main}
                <div>
                {drawers}
                {beneficiaries}
                </div>
            </div>
        );
    }

    componentDidMount() {
        this.dataLoader("Main");
        this.dataLoader("Drawers");
        this.dataLoader("Beneficiaries");
    }

    dataLoader(statesSuffix) {
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