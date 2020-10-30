import React from 'react';
import DataComponent from './data-component.jsx';

export default class ListPageComponent extends DataComponent {
    constructor(props, header, relativeApiUrl) {
        super(props, header, relativeApiUrl);

        this.state.pageSize = appStorage.paging.getItemsPerPage();
        this.state.pageNumber = 1;
        this.state.pageMaxNumberHtml = "";

        this.onPageSizeValueChanged = this.onPageSizeValueChanged.bind(this);
        this.onPageNumberValueChanged = this.onPageNumberValueChanged.bind(this);
        self = this;
    }

    getFullPathWithParameters() {
        let fullPath =
            `${this.state.url}?pageSize=${this.state.pageSize}&pageNumber=${this.state.pageNumber}`;

        return fullPath;
    }

    loadData() {
        var url = this.getFullPathWithParameters();
        super.loadData(url, function (items) {
            if(items.length < self.state.pageSize){
                self.setState({ pageMaxNumber: self.state.pageNumber });
            }
            else{
                self.setState({ pageMaxNumber: undefined });
            }
        });
    }

    onPageSizeValueChanged(e) {
        var value = parseInt(e.target.value, 10) || 5;
        this.setState({ pageSize: value });
        
        appStorage.paging.setItemsPerPage(value);
    }

    onPageNumberValueChanged(e) {
        var value = parseInt(e.target.value, 10) || 1;
        this.setState({ pageNumber: value });
    }

    componentDidUpdate(prevProps, prevState) {
        if (prevState.pageNumber !== this.state.pageNumber || prevState.pageSize !== this.state.pageSize) {
            this.loadData();
        }
    }

    renderPage(warningsArray, table) {
        return super.renderPage(
            <div>
                <div className="pager-container">
                    <span>Items per page:
                        <input type="number" min="1" value={this.state.pageSize}
                            onChange={this.onPageSizeValueChanged} />
                    </span>
                    <span>Page number:
                        <input type="number" min="1" value={this.state.pageNumber} max={this.state.pageMaxNumber}
                            onChange={this.onPageNumberValueChanged} />
                    </span>
                </div>
                <ul className="warnings">
                    {warningsArray.map(item => <li>{item}</li>)}
                </ul>
                {table}
            </div>
        );
    }
}
