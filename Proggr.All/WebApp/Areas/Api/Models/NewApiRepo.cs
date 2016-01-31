using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Areas.Api.Models
{
    public class NewApiRepo
    {
        public string FullName { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
    }
}