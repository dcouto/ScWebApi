using System;
using System.Linq;
using ScWebApi.Domain.Common;
using ScWebApi.Domain.Models.sitecore.templates.ScWebApi.Shared_Content.Items;
using Sitecore.Services.Core;
using Product = ScWebApi.Repositories.Models.SitecoreEntityService.Product;

namespace ScWebApi.Repositories.DALs.SitecoreEntityService
{
    public class ProductRepository : BaseRepository, IRepository<Product>
    {
        public void Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Product entity)
        {
            throw new NotImplementedException();
        }

        public Product FindById(string id)
        {
            var glassItem = HelperMethods.GetItemById<IProduct>(id, SitecoreContext);

            if (glassItem != null)
            {
                var listPrice = 0.00;

                double.TryParse(glassItem.List_Price, out listPrice);

                return new Product(id, glassItem.Url, glassItem.Legacy_Name, listPrice, glassItem.Description);
            }

            return null;
        }

        public IQueryable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}