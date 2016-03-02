using System;
using Glass.Mapper.Sc;
using ScWebApi.Domain.Models;

namespace ScWebApi.Domain.Common
{
    public static class HelperMethods
    {
        public static T GetItemById<T>(Guid guid, ISitecoreContext sitecoreContext) where T : class, IGlassBase
        {
            if (guid == Guid.Empty)
                return default(T);

            var glassItem = sitecoreContext.GetItem<T>(guid);

            return glassItem;
        }

        public static T GetItemById<T>(string id, ISitecoreContext sitecoreContext) where T : class, IGlassBase
        {
            Guid guid;

            if (Guid.TryParse(id, out guid))
                return GetItemById<T>(guid, sitecoreContext);

            return default(T);
        }
    }
}