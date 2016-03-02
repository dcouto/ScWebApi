using Sitecore.Services.Core.Model;

namespace ScWebApi.Repositories.Models.SitecoreEntityService
{
    public class BaseEntity : EntityIdentity
    {
        public BaseEntity()
        {
        }

        public BaseEntity(string id, string url, string legacyName)
        {
            Id = id;
            Url = url;
            LegacyName = legacyName;
        }

        public string LegacyName { get; set; }
    }
}