using System.Web.Mvc;
using Glass.Mapper.Sc.Web.Mvc;

namespace ScWebApi.Web.Controllers.Search
{
    public class SearchFormController : GlassController
    {
        // GET: SearchForm
        public ActionResult Default()
        {
            return View("~/Views/ScWebApi/Renderings/Search/SearchForm.cshtml");
        }
    }
}