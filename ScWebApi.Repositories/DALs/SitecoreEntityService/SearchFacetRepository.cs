using System;
using System.Linq;
using ScWebApi.Domain.Common;
using ScWebApi.Domain.Models;
using ScWebApi.Domain.Models.sitecore.templates.Common;
using ScWebApi.Domain.Models.sitecore.templates.ScWebApi.Shared_Content.Items;
using ScWebApi.Domain.Search.Managers;
using ScWebApi.Repository.Models.SitecoreEntityService;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Services.Core;

namespace ScWebApi.Repositories.DALs.SitecoreEntityService
{
    public class SearchFacetRepository : BaseRepository, IRepository<SearchFacet>
    {
        public IQueryable<SearchFacet> GetAll()
        {
            var sharedContentFolder =
                SitecoreContext.GetItem<ISite>(SitecoreContext.GetHomeItem<IGlassBase>().Parent.Id)
                    .GetSharedContentFolder();

            if (sharedContentFolder == null)
                return Enumerable.Empty<SearchFacet>().AsQueryable();

            var searcher = new SiteSearchManager();

            var allSearchFacets =
                searcher.Search<SearchResultItem>(SitecoreContext.Database.Name, sharedContentFolder.Id,
                    IProduct_Category_Constants.TemplateIdString)
                    .Hits.Select(i => i.Document)
                    .Select(i => SitecoreContext.GetItem<IProduct_Category>(i.ItemId.Guid))
                    .Where(i => i != null)
                    .Select(i => new SearchFacet(i.Id.ToString("N"), i.Url, i.Legacy_Name))
                    .AsQueryable();

            return allSearchFacets;
        }

        public SearchFacet FindById(string id)
        {
            var glassItem = HelperMethods.GetItemById<IProduct_Category>(id, SitecoreContext);

            if (glassItem != null)
                return new SearchFacet(id, glassItem.Url, glassItem.Legacy_Name);

            return null;
        }

        public void Add(SearchFacet entity)
        {
            throw new NotImplementedException();
        }

        public bool Exists(SearchFacet entity)
        {
            throw new NotImplementedException();
        }

        public void Update(SearchFacet entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(SearchFacet entity)
        {
            throw new NotImplementedException();
        }
    }
}