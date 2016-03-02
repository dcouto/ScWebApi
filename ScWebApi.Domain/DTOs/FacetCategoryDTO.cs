using System.Collections.Generic;

namespace ScWebApi.Domain.DTOs
{
    public class FacetCategoryDTO
    {
        public FacetCategoryDTO()
        {
            Facets = new List<SearchFacetDTO>();
        }

        public FacetCategoryDTO(string name, List<SearchFacetDTO> facets)
        {
            Name = name;
            Facets = facets;
        }

        public string Name { get; set; }
        public List<SearchFacetDTO> Facets { get; set; }
    }
}