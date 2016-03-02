using System.Linq;

namespace ScWebApi.Domain.Models.sitecore.templates.Common
{
    public static class SiteExtensions
    {
        public static IFolder GetSharedContentFolder(this ISite site)
        {
            var folderScItem = site.Children.FirstOrDefault(i => i.Name == "Shared Content");

            return folderScItem as IFolder;
        }
    }
}