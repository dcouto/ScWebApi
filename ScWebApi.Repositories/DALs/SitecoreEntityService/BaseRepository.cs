using Glass.Mapper.Sc;

namespace ScWebApi.Repositories.DALs.SitecoreEntityService
{
    public class BaseRepository
    {
        protected ISitecoreContext SitecoreContext;

        public BaseRepository()
        {
            SitecoreContext = new SitecoreContext();
        }
    }
}