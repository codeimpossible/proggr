﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using Octokit;
using Proggr.Data;
using WebApp.Areas.Api.Filters;
using WebApp.Data;

namespace WebApp.Areas.Api.Controllers
{
    [Authorize]
    public class CurrentUserController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var currentUserName = User.Identity.Name;
            var userJson = Storage.GetApiData(currentUserName, ApiStorageConstants.APIDATA_KEY_USER);

            if (userJson == null)
            {
                // get the data from the API
                var client = CreateClient();
                var user = await client.User.Current();

                Storage.StoreApiData(currentUserName, ApiStorageConstants.APIDATA_KEY_USER, user);

                return Json(User, JsonRequestBehavior.AllowGet);
            }

            return Content(userJson, "application/json", Encoding.UTF8);
        }

        public async Task<ActionResult> Repos()
        {
            var currentUserName = User.Identity.Name;
            
            // we need to fetch this from the github api
            var client = CreateClient();
            var repos = await client.Repository.GetAllForCurrent();

            // store the repos back into the github table
            Storage.StoreApiData(currentUserName, ApiStorageConstants.APIDATA_KEY_REPOSITORIES, repos);

            return Json(repos, JsonRequestBehavior.AllowGet);
        }

        private GitHubClient CreateClient()
        {
            var currentUserName = User.Identity.Name;
            var token = Storage.GetApiData(currentUserName, ApiStorageConstants.APIDATA_KEY_APITOKEN);
            if (token == null) return null;
            return new GitHubClient(new ProductHeaderValue("proggr")) {Credentials = new Credentials(token)};
        }
    }
}
