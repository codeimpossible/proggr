using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using Octokit;
using WebApp.Areas.Api.Filters;
using WebApp.Data;

namespace WebApp.Areas.Api.Controllers
{
    [Authorize]
    [CurrentUserOnly]
    public class CurrentUserController : Controller
    {
        public async Task<ActionResult> Index(string username)
        {
            var currentUserName = User.Identity.Name;
            var storage = Storage.Current();
            var user = storage.GithubJsonData.FindAllByGithubUserName(currentUserName).FirstOrDefault();

            if (user == null)
            {
                return new HttpNotFoundResult();
            }

            return Content(user.UserJsonData, "application/json", Encoding.UTF8);
        }

        public async Task<ActionResult> Repos(string username)
        {
            var currentUserName = User.Identity.Name;
            var apiData = Storage.Current().GithubJsonData.Get(currentUserName);
            if (apiData == null)
            {
                return new HttpNotFoundResult();
            }
            var reposCache = apiData.ReposJsonData;

            if (String.IsNullOrWhiteSpace(reposCache))
            {
                // we need to fetch this from the github api
                var token = apiData.ApiToken;
                var client = new GitHubClient(new ProductHeaderValue("proggr")) { Credentials = new Credentials(token) };
                var repos = await client.Repository.GetAllForUser(currentUserName);

                // store the repos back into the github table
                Storage.StoreRepositoryJson(currentUserName, repos);

                return Json(repos, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Content(reposCache, "application/json", Encoding.UTF8);
            }
        }
    }
}
