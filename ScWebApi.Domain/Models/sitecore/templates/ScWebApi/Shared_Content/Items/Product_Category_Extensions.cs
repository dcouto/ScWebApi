using System.Collections.Generic;
using System.Linq;

namespace ScWebApi.Domain.Models.sitecore.templates.ScWebApi.Shared_Content.Items
{
    public static class Product_Category_Extensions
    {
        public static IEnumerable<IProduct_Category> GetProductSubCategories(this IProduct_Category item)
        {
            return
                item.Children.Where(i => i.TemplateId == IProduct_Category_Constants.TemplateId)
                    .Select(i => i as IProduct_Category);
        }
    }
}