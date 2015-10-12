using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Octokit;
using WebApp.Areas.Api.Models;
using WebApp.Data;

namespace WebApp.Areas.Api.Controllers
{
    [Authorize]
    public class ReposController : Controller
    {
        public ActionResult Index()
        {
            var db = Storage.Current();
            var userRepos = Storage.GetApiData<List<Repository>>(User.Identity.Name, Storage.APIDATA_KEY_REPOSITORIES)
                .SelectMany(r => r.FullName).ToArray();


            // get the users repositories that are being tracked in the system, plus any public repositories
            var visibleRepos =
                db.CodeLocations.FindAll(db.CodeLocations.FullName == userRepos || db.CodeLocations.IsPublic);

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