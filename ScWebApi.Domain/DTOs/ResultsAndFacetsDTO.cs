using System.Collections.Generic;

namespace ScWebApi.Domain.DTOs
{
    public class ResultsAndFacetsDTO
    {
        public List<KeyValuePair<int, SearchResultDTO>> SortedSearchResults { get; set; }
        public List<KeyValuePair<int, FacetCategoryDTO>> SortedFacetCategories { get; set; }
        public int TotalSearchResults { get; set; }

        public ResultsAndFacetsDTO()
        {
            SortedSearchResults = new List<KeyValuePair<int, SearchResultDTO>>();
            SortedFacetCategories = new List<KeyValuePair<int, FacetCategoryDTO>>();
        }

        public ResultsAndFacetsDTO(List<KeyValuePair<int, SearchResultDTO>> sortedSearchResults,
            List<KeyValuePair<int, FacetCategoryDTO>> facetCategories, int totalSearchResults)
        {
            SortedSearchResults = sortedSearchResults;
            SortedFacetCategories = facetCategories;
            TotalSearchResults = totalSearchResults;
        }
    }
}