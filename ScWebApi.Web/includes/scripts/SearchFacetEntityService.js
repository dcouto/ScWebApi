var SearchFacetEntityService = {
    service: new EntityService({
        url: "/sitecore/api/ssc/ScWebApi-Web-Controllers-SitecoreEntityService/SearchFacet"
    }),

    getAll: function() {
        var allFacets;

        this.service.fetchEntities().execute().then(function(searchFacets) {
            allFacets = searchFacets;
            done();
        });

        return allFacets;
    },

    findById: function(id) {
        var facet;

        this.service.fetchEntity(id).execute().then(function(searchFacet) {
            facet = searchFacet;
            done();
        });

        return facet;
    }
};