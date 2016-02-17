using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Areas.Api.Models;
using WebApp.Data;
using WebApp.Services;

namespace WebApp.Areas.Api.Controllers
{
    public class EmailAddressesController : Controller
    {
        private readonly IGithubApiDataCacheService _apiDataCacheService;

        public EmailAddressesController() : this(null) { }

        public EmailAddressesController(IGithubApiDataCacheService apiDataCacheService)
        {
            _apiDataCacheService = apiDataCacheService ?? new GithubApiDataCacheService();
        }

        // GET: Api/EmailAddresses
        public ActionResult Index()
        {
            //var matches = (List<UserEmailAddressMatch>) Storage.Current().GetUnClaimedEmailAddresses();

            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}