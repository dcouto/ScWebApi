using System.Web.Mvc;
using ScWebApi.Repositories.DALs.SitecoreEntityService;
using ScWebApi.Repositories.Models.SitecoreEntityService;
using Sitecore.Services.Core;
using Sitecore.Services.Infrastructure.Sitecore.Services;

namespace ScWebApi.Web.Controllers.SitecoreEntityService
{
    [ValidateAntiForgeryToken]
    [ServicesController]
    public class SearchFacetController : EntityService<SearchFacet>
    {
        public SearchFacetController(IRepository<SearchFacet> repository) : base(repository)
        {
        }

        public SearchFacetController() : this(new SearchFacetRepository())
        {
        }
    }
}