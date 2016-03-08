using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Glass.Mapper.Sc;
using ScWebApi.Domain.Models.sitecore.templates.ScWebApi.Pages;
using ScWebApi.Domain.Models.sitecore.templates.ScWebApi.Shared_Content.Folders;
using ScWebApi.Domain.Models.sitecore.templates.ScWebApi.Shared_Content.Items;
using ScWebApi.Domain.Models.sitecore.templates.ScWebApi.Shared_Content.Items.Base;
using ScWebApi.Repositories.DALs.AdventureWorks;
using ScWebApi.Repositories.Models.AdventureWorks;
using Sitecore;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Layouts;
using Sitecore.SecurityModel;

namespace ScWebApi.Repositories.DALs.Sitecore
{
    public class SitecoreRepository
    {
        private ISitecoreContext _sitecoreContext;

        public SitecoreRepository(ISitecoreContext sitecoreContext)
        {
            _sitecoreContext = sitecoreContext;
        }

        public void AddProductCategories(Guid productCategroiesFolder)
        {
            using (new BulkUpdateContext())
            {
                using (new SecurityDisabler())
                {
                    var prodCatsFolder =
                        _sitecoreContext.GetItem<IProduct_Categories_Folder>(productCategroiesFolder);

                    if (prodCatsFolder == null)
                        return;

                    var prodCats = prodCatsFolder.GetProductCategories(_sitecoreContext).ToList();

                    using (var db = new AdventureWorksContext())
                    {
                        foreach (var importingProdCat in db.ProductCategories.ToList())
                        {
                            var matchingScProdCat =
                                prodCats.FirstOrDefault(
                                    i => i.Legacy_ID == importingProdCat.ProductCategoryID.ToString());

                            if (matchingScProdCat == null)
                                matchingScProdCat =
                                    _sitecoreContext.Create<IProduct_Category, IProduct_Categories_Folder>(
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
                                        _sitecoreContext.Create<IProduct_Category, IProduct_Category>(matchingScProdCat,
                                            ItemUtil.ProposeValidItemName(importingSubCategory.Name));

                                matchingScSubProdCat.Legacy_ID = importingSubCategory.ProductSubcategoryID.ToString();
                                matchingScSubProdCat.Legacy_Name = importingSubCategory.Name;
                            }
                        }
                    }
                }
            }
        }

        public void AddProducts(Guid productsLandingPage, IProduct_Categories_Folder productCategoriesFolder)
        {
            using (new BulkUpdateContext())
            {
                using (new SecurityDisabler())
                {
                    var prodsLandingPage = _sitecoreContext.GetItem<IProducts_Landing_Page>(productsLandingPage);

                    if (prodsLandingPage == null)
                        return;

                    var existingProducts = prodsLandingPage.GetProducts<IProduct>(_sitecoreContext).ToList();

                    var productCategories = productCategoriesFolder
                            .GetProductCategories(_sitecoreContext)
                            .SelectMany(i => i.GetProductSubCategories().Select(j => j).ToList()).ToList();

                    using (var db = new AdventureWorksContext())
                    {
                        var allProductsAndDescriptions = db.vProductAndDescriptions.Where(i => i.CultureID == "en");





                        foreach (var importingProd in db.Products.ToList())
                        {
                            var matchingProd = GetMatchingProduct(existingProducts, importingProd, prodsLandingPage);

                            if (matchingProd == null)
                                continue;

                            matchingProd.Legacy_ID = importingProd.ProductID.ToString();
                            matchingProd.Legacy_Name = importingProd.Name;
                            matchingProd.List_Price = importingProd.ListPrice.ToString(CultureInfo.InvariantCulture);

                            var matchingSubCategory =
                                productCategories.FirstOrDefault(
                                    i => i.Legacy_ID == importingProd.ProductSubcategoryID.ToString());

                            if (matchingSubCategory != null)
                                matchingProd.Product_Subcategory = matchingSubCategory.Id;

                            //_sitecoreContext.Save(matchingProd);







                            var matchingProdAndDesc =
                                allProductsAndDescriptions.FirstOrDefault(i => i.ProductID == importingProd.ProductID);

                            if (matchingProdAndDesc != null)
                            {
                                //var matchingProdRaw = _sitecoreContext.GetItem<IProduct_Raw>(matchingProd.Id);
                                //var matchingProdRaw = _sitecoreContext.GetItem<IProduct>(matchingProd.Id);

                                //matchingProdRaw.Description_Raw = string.Format("<p>{0}</p>", matchingProdAndDesc.Description);
                                //matchingProdRaw.Description = string.Format("<p>{0}</p>", matchingProdAndDesc.Description);
                                matchingProd.Description_Raw = string.Format("<p>{0}</p>", matchingProdAndDesc.Description);

                                //_sitecoreContext.Save(matchingProdRaw);
                            }



                            _sitecoreContext.Save(matchingProd);
                        }
                    }
                }
            }
        }

        public void AddProductDescriptions(Guid productsLandingPage)
        {
            using (new BulkUpdateContext())
            {
                using (new SecurityDisabler())
                {
                    var prodsLandingPage = _sitecoreContext.GetItem<IProducts_Landing_Page>(productsLandingPage);

                    if (prodsLandingPage == null)
                        return;

                    //var existingProducts = prodsLandingPage.GetProducts<IProduct_Raw>(_sitecoreContext).ToList();
                    var existingProducts = prodsLandingPage.GetProducts<IProduct>(_sitecoreContext).ToList();

                    using (var db = new AdventureWorksContext())
                    {
                        var allProductsAndDescriptions = db.vProductAndDescriptions.Where(i => i.CultureID == "en");

                        foreach (var importingProd in db.Products.ToList())
                        {
                            var matchingProd = GetMatchingProduct(existingProducts, importingProd, prodsLandingPage);

                            var matchingProdAndDesc =
                                allProductsAndDescriptions.FirstOrDefault(i => i.ProductID == importingProd.ProductID);

                            if (matchingProdAndDesc != null)
                            {
                                //var matchingProdRaw = _sitecoreContext.GetItem<IProduct_Raw>(matchingProd.Id);
                                var matchingProdRaw = _sitecoreContext.GetItem<IProduct>(matchingProd.Id);

                                matchingProdRaw.Description_Raw = string.Format("<p>{0}</p>", matchingProdAndDesc.Description);
                                //matchingProdRaw.Description = string.Format("<p>{0}</p>", matchingProdAndDesc.Description);

                                _sitecoreContext.Save(matchingProdRaw);
                            }
                        }
                    }
                }
            }
        }

        private T GetMatchingProduct<T>(List<T> existingProducts, Product importingProd, IProducts_Landing_Page prodsLandingPage) where T : class, IBase_Imported_Content
        {
            var matchingProd = existingProducts.FirstOrDefault(i => i.Legacy_ID == importingProd.ProductID.ToString());

            if (matchingProd == null)
            {
                matchingProd = _sitecoreContext.Create<T, IProducts_Landing_Page>(
                    prodsLandingPage,
                    ItemUtil.ProposeValidItemName(importingProd.Name));
            }

            return matchingProd;
        }
    }
}
