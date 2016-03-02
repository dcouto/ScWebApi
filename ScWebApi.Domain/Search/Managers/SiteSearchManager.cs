using System;
using System.Collections.Generic;
using System.Linq;
using ScWebApi.Domain.DTOs;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data;

namespace ScWebApi.Domain.Search.Managers
{
    public class SiteSearchManager : BaseSearchManager
    {
        public SearchResultsDTO<T> Search<T>(string databaseName, Guid ancestorId = new Guid(), string templateId = null,
            string keywords = null, int resultsPerPage = int.MaxValue, int page = 0) where T : SearchResultItem
        {
            return Search<T>(databaseName, ID.Parse(ancestorId), new List<string> {templateId}, keywords, resultsPerPage,
                page);
        }

        public SearchResultsDTO<T> Search<T>(string databaseName, Guid ancestorId = new Guid(),
            IEnumerable<string> templateIds = null, string keywords = null, int resultsPerPage = int.MaxValue,
            int page = 0) where T : SearchResultItem
        {
            return Search<T>(databaseName, ID.Parse(ancestorId), templateIds, keywords, resultsPerPage, page);
        }

        public SearchResultsDTO<T> Search<T>(string databaseName, ID ancestorId = null,
            IEnumerable<string> templateIds = null, string keywords = null, int resultsPerPage = int.MaxValue,
            int page = 0) where T : SearchResultItem
        {
            var query = PredicateBuilder.True<T>();

            AddAncestorCondition(ref query, ancestorId);

            AddTemplatesCondition(ref query, templateIds);

            AddKeywordsCondition(ref query, keywords);

            using (
                var searcher =
                    ContentSearchManager.GetIndex(string.Format("sitecore_{0}_index", databaseName))
                        .CreateSearchContext())
            {
                var searchResults = searcher.GetQueryable<T>().Where(query);

                searchResults = searchResults.Skip(page*resultsPerPage).Take(resultsPerPage);

                var scResultsAndFacets = searchResults.GetResults();

                var facetResults = scResultsAndFacets.Facets ?? new FacetResults();

                return new SearchResultsDTO<T>(facetResults.Categories.ToList(), scResultsAndFacets.Hits.ToList(),
                    scResultsAndFacets.TotalSearchResults);
            }
        }
    }
}