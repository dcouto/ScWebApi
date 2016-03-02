function viewModel() {
    var self = this;

    self.totalSearchResults = ko.observable(0);

    self.searchResults = ko.observableArray();
    self.orderedSearchResults = ko.pureComputed(function() {
        self.searchResults.sort(function(a, b) {
            return parseInt(a.sortOrder) - parseInt(b.sortOrder);
        });

        return self.searchResults();
    });

    self.facetCategories = ko.observableArray();
    self.orderedFacetCategories = ko.pureComputed(function() {
        var orderedList = [];

        for (var i = 0; i < self.facetCategories().length; i++) {

            self.facetCategories()[i].facets().sort(function(a, b) {
                return parseInt(a.sortOrder) - parseInt(b.sortOrder);
            });

            orderedList.push(self.facetCategories()[i]);
        }

        return orderedList;
    });
}

var vm = new viewModel();

ko.applyBindings(vm);

var serviceUrlPrefix = "/sitecore/api/ssc/ScWebApi-Web-Controllers-SitecoreEntityService";

function setSearchResults() {
    if (resultsAndFacetsJson.SortedSearchResults != undefined) {
        for (var i = 0; i < resultsAndFacetsJson.SortedSearchResults.length; i++) {
            var currentSearchResult = resultsAndFacetsJson.SortedSearchResults[i];

            (function(x) {
                var productsEntity = new EntityService({ url: serviceUrlPrefix + "/Product" });

                productsEntity.fetchEntity(x.Value.ItemId).execute().then(function(response) {
                    var newSearchResult = {
                        name: response.LegacyName,
                        listPrice: response.ListPrice.toFixed(2),
                        description: response.Description,
                        sortOrder: x.Key
                    };

                    vm.searchResults.push(newSearchResult);
                });
            })(currentSearchResult);
        }
    }
}

function setFacetCategories() {
    if (resultsAndFacetsJson.SortedFacetCategories != undefined) {
        for (var i = 0; i < resultsAndFacetsJson.SortedFacetCategories.length; i++) {
            var currentFacetCategory = resultsAndFacetsJson.SortedFacetCategories[i].Value;

            var newFacetCategory = {
                name: currentFacetCategory.Name,
                facets: ko.observableArray()
            };

            for (var j = 0; j < currentFacetCategory.Facets.length; j++) {
                var currentFacet = currentFacetCategory.Facets[j];

                (function(x) {
                    var searchFacetsEntity = new EntityService({ url: serviceUrlPrefix + "/SearchFacet" });

                    searchFacetsEntity.fetchEntity(x.Name).execute().then(function(response) {
                        var newFacet = {
                            id: x.Name,
                            name: response.LegacyName,
                            count: x.Count,
                            sortOrder: x.SortOrder
                        };

                        newFacetCategory.facets.push(newFacet);
                    });
                })(currentFacet);
            }

            vm.facetCategories.push(newFacetCategory);
        }
    }
}

$(function() {
    if (resultsAndFacetsJson == undefined)
        return;

    if (resultsAndFacetsJson.TotalSearchResults != undefined) {
        vm.totalSearchResults(resultsAndFacetsJson.TotalSearchResults);
    }

    setSearchResults();

    setFacetCategories();
});