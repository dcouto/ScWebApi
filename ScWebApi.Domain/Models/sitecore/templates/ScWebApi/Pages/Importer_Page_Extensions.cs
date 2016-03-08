using Glass.Mapper.Sc;
using ScWebApi.Domain.Models.sitecore.templates.ScWebApi.Shared_Content.Folders;

namespace ScWebApi.Domain.Models.sitecore.templates.ScWebApi.Pages
{
    public static class Importer_Page_Extensions
    {
        public static IProduct_Categories_Folder GetProductCategoriesFolder(this IImporter_Page item,
            ISitecoreContext serviceContext)
        {
            return serviceContext.GetItem<IProduct_Categories_Folder>(item.Product_Categories_Folder);
        }
    }
}