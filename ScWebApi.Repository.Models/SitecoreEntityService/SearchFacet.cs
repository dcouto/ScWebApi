namespace ScWebApi.Repository.Models.SitecoreEntityService
{
    public class SearchFacet : BaseEntity
    {
        public SearchFacet()
        {
        }

        public SearchFacet(string id, string url, string legacyName) : base(id, url, legacyName)
        {
        }
    }
}