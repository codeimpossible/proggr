﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebApp.Areas.Api.Models;
using WebApp.Data;
using Proggr.Data;
using Proggr.Data.Models;
using Simple.Data;
using WebApp.Services;

namespace WebApp.Areas.Api.Controllers
{
    [Authorize]
    public class ReposController : Controller
    {
        private readonly IGithubApiDataCacheService _githubApiDataCache;

        public ReposController() : this(null) { }

        public ReposController(IGithubApiDataCacheService githubApiDataCache)
        {
            _githubApiDataCache = githubApiDataCache ?? new GithubApiDataCacheService();
        }

        public ActionResult Index()
        {
            var db = Storage.CreateConnection();
            var dbRepos = _githubApiDataCache.GetApiData<List<GithubApiRepository>>(User.Identity.Name, ApiStorageConstants.APIDATA_KEY_REPOSITORIES);
            var userRepos = dbRepos.Select(r => r.FullName).ToArray();


            // get the users repositories that are being tracked in the system, plus any public repositories
            var visibleRepos =
                (List<CodeLocation>)db.CodeLocations.FindAll(db.CodeLocations.FullName == userRepos || db.CodeLocations.IsPublic == true);

            return Json(visibleRepos, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(CodeLocation newCodeLocation)
        {
            if (newCodeLocation.Id != Guid.Empty)
            {
                Response.StatusCode = (int) HttpStatusCode.PreconditionFailed;
                return Json(new PreconditionFailedException() { Message = "Id cannot have a value when creating a CodeLocation" });
            }
            
            var db = Storage.CreateConnection();

            var codeLocation = (CodeLocation)db.CodeLocations.Insert(newCodeLocation);

            Response.StatusCode = (int)HttpStatusCode.Created;

            return Json(codeLocation);
        }
    }
}