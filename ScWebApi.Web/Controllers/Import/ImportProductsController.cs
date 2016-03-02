using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web.Mvc;
using ScWebApi.Domain.Models.sitecore.templates.ScWebApi.Pages;
using ScWebApi.Domain.Models.sitecore.templates.ScWebApi.Shared_Content.Folders;
using ScWebApi.Domain.Models.sitecore.templates.ScWebApi.Shared_Content.Items;
using ScWebApi.Repositories.DALs.AdventureWorks;
using ScWebApi.Web.ViewModels.Import;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;

namespace ScWebApi.Web.Controllers.Import
{
    public class ImportProductsController : GlassController<IImporter_Page>
    {
        //private readonly string _connString = ConfigurationManager.ConnectionStrings["AdventureWorks"].ConnectionString;

        public ImportProductsController()
        {
            SitecoreContext = new SitecoreContext(Factory.GetDatabase("master"));
        }

        // GET: ImportProducts
        public ActionResult Default(ImportProductsViewModel vm)
        {
            switch (vm.Task)
            {
                case "productCategories":
                    AddProductCategories();

                    break;

                case "products":
                    AddProducts();

                    break;
            }

            return View("~/Views/ScWebApi/Renderings/Import/ImportProducts.cshtml");
        }

        private void AddProductCategories()
        {
            using (new BulkUpdateContext())
            {
                using (new SecurityDisabler())
                {
                    var prodCatsFolder =
                        SitecoreContext.GetItem<IProduct_Categories_Folder>(Context.Product_Categories_Folder);

                    if (prodCatsFolder == null)
                        return;

                    var prodCats = prodCatsFolder.GetProductCategories(SitecoreContext).ToList();

                    using (var db = new AdventureWorksContext())
                    {
                        foreach (var importingProdCat in db.ProductCategories.ToList())
                        {
                            var matchingScProdCat =
                                prodCats.FirstOrDefault(
                                    i => i.Legacy_ID == importingProdCat.ProductCategoryID.ToString());

                            if (matchingScProdCat == null)
                                matchingScProdCat =
                                    SitecoreContext.Create<IProduct_Category, IProduct_Categories_Folder>(
                                        prodCatsFolder, ItemUtil.ProposeValidItemName(importingProdCat.Name));

                            matchingScProdCat.Legacy_ID = importingProdCat.ProductCategoryID.ToString();
                            matchingScProdCat.Legacy_Name = importingProdCat.Name;

                            var prodSubCats = matchingScProdCat.GetProductSubCategories().ToList();

                            foreach (var importingSubCategory in importingProdCat.ProductSubcategories.ToList())
                            {
                                var matchingScSubProdCat =
                                    prodSubCats.FirstOrDefault(
                                        i => i.Legacy_ID == importingSubCategory.ProductSubcategoryID.ToString());

                                if (matchingScSubProdCat == null)
                                    matchingScSubProdCat =
                                        SitecoreContext.Create<IProduct_Category, IProduct_Category>(matchingScProdCat,
                                            ItemUtil.ProposeValidItemName(importingSubCategory.Name));

                                matchingScSubProdCat.Legacy_ID = importingSubCategory.ProductSubcategoryID.ToString();
                                matchingScSubProdCat.Legacy_Name = importingSubCategory.Name;
                            }
                        }
                    }
                }
            }
        }

        private void AddProducts()
        {
            using (new BulkUpdateContext())
            {
                using (new SecurityDisabler())
                {
                    var prodsLandingPage = SitecoreContext.GetItem<IProducts_Landing_Page>(Context.Products_Landing_Page);

                    if (prodsLandingPage == null)
                        return;

                    var existingProducts = prodsLandingPage.GetProducts().ToList();

                    var productCategories =
                        Context.GetProductCategoriesFolder(SitecoreContext)
                            .GetProductCategories(SitecoreContext)
                            .SelectMany(i => i.GetProductSubCategories().Select(j => j).ToList()).ToList();

                    using (var db = new AdventureWorksContext())
                    {
                        var allProductsAndDescriptions = db.vProductAndDescriptions.Where(i => i.CultureID == "en");

                        foreach (var importingProd in db.Products.ToList())
                        {
                            var matchingProd =
                                existingProducts.FirstOrDefault(i => i.Legacy_ID == importingProd.ProductID.ToString());

                            if (matchingProd == null)
                                matchingProd = SitecoreContext.Create<IProduct, IProducts_Landing_Page>(
                                    prodsLandingPage,
                                    ItemUtil.ProposeValidItemName(importingProd.Name));

                            matchingProd.Legacy_ID = importingProd.ProductID.ToString();
                            matchingProd.Legacy_Name = importingProd.Name;
                            matchingProd.List_Price = importingProd.ListPrice.ToString(CultureInfo.InvariantCulture);

                            var matchingProdAndDesc =
                                allProductsAndDescriptions.FirstOrDefault(i => i.ProductID == importingProd.ProductID);

                            if (matchingProdAndDesc != null)
                                matchingProd.Description = string.Format("<p>{0}</p>", matchingProdAndDesc.Description);

                            var matchingSubCategory =
                                productCategories.FirstOrDefault(
                                    i => i.Legacy_ID == importingProd.ProductSubcategoryID.ToString());

                            if (matchingSubCategory != null)
                                matchingProd.Product_Subcategory = matchingSubCategory.Id;

                            SitecoreContext.Save(matchingProd);
                        }
                    }
                }
            }
        }
    }
}