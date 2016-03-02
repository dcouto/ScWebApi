using System.Collections.Generic;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.SearchTypes;

namespace ScWebApi.Domain.DTOs
{
    public class SearchResultsDTO<T> where T : SearchResultItem
    {
        public SearchResultsDTO()
        {
        }

        public SearchResultsDTO(List<FacetCategory> facets, IEnumerable<SearchHit<T>> hits, int totalSearchResults)
        {
            FacetCategories = facets;
            Hits = hits;
            TotalSearchResults = totalSearchResults;
        }

        public List<FacetCategory> FacetCategories { get; set; }
        public IEnumerable<SearchHit<T>> Hits { get; set; }
        public int TotalSearchResults { get; set; }
    }
}