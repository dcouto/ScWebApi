using System.Web.Mvc;
using ScWebApi.Repositories.DALs.SitecoreEntityService;
using ScWebApi.Repositories.Models.SitecoreEntityService;
using Sitecore.Services.Core;
using Sitecore.Services.Infrastructure.Sitecore.Services;

namespace ScWebApi.Web.Controllers.SitecoreEntityService
{
    [ValidateAntiForgeryToken]
    [ServicesController]
    public class ProductController : EntityService<Product>
    {
        public ProductController(IRepository<Product> repository) : base(repository)
        {
        }

        public ProductController() : this(new ProductRepository())
        {
        }
    }
}