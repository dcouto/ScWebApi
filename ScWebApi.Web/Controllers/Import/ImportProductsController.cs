using System.Web.Mvc;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web.Mvc;
using ScWebApi.Domain.Models.sitecore.templates.ScWebApi.Pages;
using ScWebApi.Repositories.DALs.Sitecore;
using ScWebApi.Web.ViewModels.Import;
using Sitecore.Configuration;

namespace ScWebApi.Web.Controllers.Import
{
    public class ImportProductsController : GlassController<IImporter_Page>
    {
        private readonly SitecoreRepository _sitecoreRepository;

        public ImportProductsController()
        {
            SitecoreContext = new SitecoreContext(Factory.GetDatabase("master"));
            _sitecoreRepository = new SitecoreRepository(SitecoreContext);
        }

        // GET: ImportProducts
        public ActionResult Default(ImportProductsViewModel vm)
        {
            switch (vm.Task)
            {
                case "productCategories":
                    _sitecoreRepository.AddProductCategories(Context.Product_Categories_Folder);
                    break;

                case "products":
                    _sitecoreRepository.AddProducts(Context.Products_Landing_Page, Context.GetProductCategoriesFolder(SitecoreContext));
                    break;
            }

            return View("~/Views/ScWebApi/Renderings/Import/ImportProducts.cshtml");
        }
    }
}