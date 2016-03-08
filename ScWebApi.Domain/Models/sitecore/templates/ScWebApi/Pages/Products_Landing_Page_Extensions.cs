using System.Collections.Generic;
using System.Linq;
using ScWebApi.Domain.Models.sitecore.templates.ScWebApi.Shared_Content.Items;

namespace ScWebApi.Domain.Models.sitecore.templates.ScWebApi.Pages
{
    public static class Products_Landing_Page_Extensions
    {
        public static IEnumerable<IProduct> GetProducts(this IProducts_Landing_Page item)
        {
            return item.Children.Where(i => i.TemplateId == IProduct_Constants.TemplateId)
                .Select(i => i as IProduct);
        }
    }
}