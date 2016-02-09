using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebApp.Areas.Api.Models;
using WebApp.Data;
using Proggr.Data;
using Proggr.Data.Models;

namespace WebApp.Areas.Api.Controllers
{
    [Authorize]
    public class ReposController : Controller
    {
        public ActionResult Index()
        {
            var db = Storage.Current();
            var dbRepos = Storage.GetApiData<List<GithubApiRepository>>(User.Identity.Name, ApiStorageConstants.APIDATA_KEY_REPOSITORIES);
            var userRepos = dbRepos.Select(r => r.FullName).ToArray();


            // get the users repositories that are being tracked in the system, plus any public repositories
            var visibleRepos =
                db.CodeLocations.FindAll(db.CodeLocations.FullName == userRepos || db.CodeLocations.IsPublic == true);

            return Json(visibleRepos, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(NewApiRepo newRepo)
        {
            var db = Storage.Current();

            var repo = db.CodeLocations.Insert(newRepo);

            Response.StatusCode = (int)HttpStatusCode.Created;

            return Json(repo);
        }
    }
}