using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Glass.Mapper.Sc.Web.Mvc;
using ScWebApi.Domain.Common;
using ScWebApi.Domain.DTOs;
using ScWebApi.Domain.Models.sitecore.templates.ScWebApi.Pages;
using ScWebApi.Domain.Models.sitecore.templates.ScWebApi.Shared_Content.Folders;
using ScWebApi.Domain.Models.sitecore.templates.ScWebApi.Shared_Content.Items;
using ScWebApi.Domain.Search.Managers;
using ScWebApi.Web.ViewModels.Search;

namespace ScWebApi.Web.Controllers.Search
{
    public class SearchResultsController : GlassController<IProducts_Landing_Page>
    {
        // GET: SearchResults
        public ActionResult Default(SearchResultsViewModel vm)
        {
            var resultsAndFacets = new ProductsSearchManager().Search(SitecoreContext.Database.Name, ContextItem.ID,
                IProduct_Constants.TemplateIdString, vm.Q, 10, 0);

            var resultsAndFacetsDTO = new ResultsAndFacetsDTO();

            // total search results count
            resultsAndFacetsDTO.TotalSearchResults = resultsAndFacets.TotalSearchResults;

            // facet categories
            for (var i = 0; i < resultsAndFacets.FacetCategories.Count; i++)
            {
                var currentFacetCategory = resultsAndFacets.FacetCategories[i];

                var facetCategoryDTO = new FacetCategoryDTO();

                switch (currentFacetCategory.Name)
                {
                    case "product_subcategory":
                        var productCategoryFolder =
                            HelperMethods.GetItemById<IProduct_Categories_Folder>(Context.Product_Categories_Folder,
                                SitecoreContext);

                        if (productCategoryFolder != null)
                        {
                            facetCategoryDTO.Name = productCategoryFolder.Category_Name;
                        }
                        else
                        {
                            facetCategoryDTO.Name = currentFacetCategory.Name;
                        }

                        break;
                }

                // facets
                if (facetCategoryDTO.Facets == null)
                    facetCategoryDTO.Facets = new List<SearchFacetDTO>();

                var facets = currentFacetCategory.Values.OrderBy(x => x.Name).ToList();

                for (var j = 0; j < facets.Count; j++)
                {
                    var currentItem = facets[j];

                    facetCategoryDTO.Facets.Add(new SearchFacetDTO(currentItem.Name, currentItem.AggregateCount, j));
                }

                // facet categories
                resultsAndFacetsDTO.SortedFacetCategories.Add(new KeyValuePair<int, FacetCategoryDTO>(i,
                    facetCategoryDTO));
            }

            // search results
            if (resultsAndFacetsDTO.SortedSearchResults == null)
                resultsAndFacetsDTO.SortedSearchResults = new List<KeyValuePair<int, SearchResultDTO>>();

            var docs = resultsAndFacets.Hits.Select(i => i.Document).ToList();

            for (var i = 0; i < docs.Count; i++)
            {
                var currentItem = docs[i];

                var newSearchResultDto = new SearchResultDTO(currentItem.ItemId.ToString(), currentItem.Name, i);

                resultsAndFacetsDTO.SortedSearchResults.Add(new KeyValuePair<int, SearchResultDTO>(i, newSearchResultDto));
            }

            vm.ResultsAndFacetsJson = new JavaScriptSerializer().Serialize(resultsAndFacetsDTO);

            return View("~/Views/ScWebApi/Renderings/Search/SearchResults.cshtml", vm);
        }
    }
}