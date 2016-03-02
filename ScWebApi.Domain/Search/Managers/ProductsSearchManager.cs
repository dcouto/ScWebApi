using System.Collections.Generic;
using System.Linq;
using ScWebApi.Domain.DTOs;
using ScWebApi.Domain.Search.ResultItems;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.Data;

namespace ScWebApi.Domain.Search.Managers
{
    public class ProductsSearchManager : BaseSearchManager
    {
        public SearchResultsDTO<ProductResultItem> Search(string databaseName, ID ancestorId, string templateId,
            string keywords, int resultsPerPage, int page)
        {
            return Search(databaseName, ancestorId, new List<string> {templateId}, keywords, resultsPerPage, page);
        }

        public SearchResultsDTO<ProductResultItem> Search(string databaseName, ID ancestorId,
            IEnumerable<string> templateIds, string keywords, int resultsPerPage, int page)
        {
            var query = PredicateBuilder.True<ProductResultItem>();

            AddAncestorCondition(ref query, ancestorId);

            AddTemplatesCondition(ref query, templateIds);

            AddKeywordsCondition(ref query, keywords);

            using (
                var searcher =
                    ContentSearchManager.GetIndex(string.Format("sitecore_{0}_index", databaseName))
                        .CreateSearchContext())
            {
                IQueryable<ProductResultItem> searchResults;

                searchResults = searcher.GetQueryable<ProductResultItem>()
                    .Where(query)
                    .FacetOn(i => i.ProductSubcategory);

                if (resultsPerPage > 0)
                    searchResults = searchResults.Skip(page*resultsPerPage).Take(resultsPerPage);

                var scResultsAndFacets = searchResults.GetResults();

                return new SearchResultsDTO<ProductResultItem>(scResultsAndFacets.Facets.Categories.ToList(),
                    scResultsAndFacets.Hits.ToList(), scResultsAndFacets.TotalSearchResults);
            }
        }
    }
}