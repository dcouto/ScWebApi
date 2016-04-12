using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace ScWebApi.Domain.Models.sitecore.templates.ScWebApi.Pages
{
    public partial interface IHome_Page
    {
        [SitecoreChildren(InferType = true)]
        IEnumerable<IImporter_Page> InterfaceChildren { get; set; } 
    }
}

namespace ScWebApi.Domain.Models.sitecore.templates.ScWebApi.Pages
{
    public static class Home_Page_Extensions
    {
        public static IEnumerable<IImporter_Page> GetChildren(this IHome_Page item)
        {
            return item.Children.Select(i => i as IImporter_Page);
        }
    }
}
