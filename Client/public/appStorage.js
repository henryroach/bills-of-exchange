window.appStorage = {
    routing: {
        isFirstLoad: true,
        getLastRoute() {
            var result = localStorage.getItem("routing.routePath");
            console.log("CURRENT ROUTE PATH: " + result);
            return result;
        },
        setLastRoute(value) {
            localStorage.setItem("routing.routePath", value);
            this.isFirstLoad = false;
            console.log("CURRENT ROUTE PATH WAS SET TO: " + value);
        },
        clearLastRoute() {
            localStorage.removeItem("routing.routePath");
            this.isFirstLoad = false;
            console.log("CURRENT ROUTE PATH WAS REMOVED");
        }
    },
    paging: {
        setItemsPerPage(count) {
            localStorage.setItem("paging.itemsPerPage", count);
        },
        getItemsPerPage() {
            var result = localStorage.getItem("paging.itemsPerPage");
            return result || 5;
        }
    },

    api: {
        rootUrl: "https://localhost:44362/",
        getFullUrl(relativeApiUrl) {
            let url = relativeApiUrl.startsWith(this.rootUrl)
                ? relativeApiUrl
                : `${this.rootUrl}${relativeApiUrl.replace(/^\//, '')}`;

            return url;
        }
    }

    // read(varName) {
    //     return localStorage.getItem(varName);
    // },
    // write(varName, value) {
    //     localStorage.setItem(varName, value);
    // }
}