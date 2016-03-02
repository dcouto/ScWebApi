using System.Collections.Generic;
using System.Linq;
using Glass.Mapper.Sc;
using ScWebApi.Domain.Models.sitecore.templates.ScWebApi.Shared_Content.Items;
using ScWebApi.Domain.Search.Managers;
using Sitecore.ContentSearch.SearchTypes;

namespace ScWebApi.Domain.Models.sitecore.templates.ScWebApi.Shared_Content.Folders
{
    public static class Product_Categories_FolderExtensions
    {
        public static IEnumerable<IProduct_Category> GetProductCategories(this IProduct_Categories_Folder item,
            ISitecoreContext sitecoreContext)
        {
            return
                item.Children.Where(i => i.TemplateId == IProduct_CategoryConstants.TemplateId.Guid)
                    .Select(i => i as IProduct_Category);
        }

        public static IEnumerable<IProduct_Category> GetAllDescendents(this IProduct_Categories_Folder item,
            ISitecoreContext sitecoreContext)
        {
            var searcher = new SiteSearchManager();

            var allDescendents = searcher.Search<SearchResultItem>(sitecoreContext.Database.Name, item.Id,
                IProduct_CategoryConstants.TemplateIdString);

            return
                allDescendents.Hits.Select(i => i.Document)
                    .Select(i => sitecoreContext.GetItem<IProduct_Category>(i.ItemId.Guid));
        }
    }
}