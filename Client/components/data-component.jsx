import React from 'react';

export default class DataComponent extends React.Component {
    constructor(props, header, relativeApiUrl) {
        super(props);

        appStorage.routing.setLastRoute(props.history.location.pathname);

        let url = appStorage.api.getFullUrl(relativeApiUrl);

        this.state = {
            header: header,
            url: url,
            error: null,
            isLoaded: false,
            data: null
        };
    }

    loadData(url, callback) {
        url = url || this.state.url;

        fetch(url).then((response) => {
            return response.json();
        }).then(
            (result) => {
                // Data was loaded successfully
                if (result.errorcode === undefined) {
                    this.setState({
                        isLoaded: true,
                        data: result,
                        error: null
                    });
                    callback && callback(result);
                }
                // Error handler
                else {
                    var errorMessage = result.errorcode === 'error_001'
                        ? `Problémy s daty: ${result.message}`
                        : "Can't load the data: an error occurs on server";

                    console.error(result.message);

                    this.setState({
                        isLoaded: true,
                        data: null,
                        error: errorMessage
                    });
                }
            },
            (error) => {
                let errorMessage = "Can't load the data: connection is failed";
                console.error(errorMessage);
                this.setState({
                    isLoaded: true,
                    data: null,
                    error: errorMessage
                });
            }
        ).catch(function (error) {
            let errorMessage = "Can't load the data: unknown error";
            console.error(errorMessage);
            this.setState({
                isLoaded: true,
                data: null,
                error: errorMessage
            });
        });
    }

    renderPage(content) {
        const { error, isLoaded, data } = this.state;
        const header = <h2>{this.state.header}</h2>;
        if (error) {
            return <div>{header} {error}</div>;
        }
        else if (!isLoaded) {
            return <div>{header} Loading...</div>;
        }
        else {
            return (
                <div>
                    {header}
                    {content}
                </div>
            );
        }
    }
}
