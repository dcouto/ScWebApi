using Glass.Mapper.Sc;
using ScWebApi.Domain.Models;

namespace ScWebApi.Domain.Common.Extensions
{
    public static class SitecoreContextExtensions
    {
        public static T GetSiteItem<T>(this ISitecoreContext sitecoreContext) where T : class, IGlassBase
        {
            return sitecoreContext.GetHomeItem<IGlassBase>().Parent as T;
        }
    }
}