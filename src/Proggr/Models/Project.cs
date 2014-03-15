using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proggr.Models
{
    public class Project
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string owner_id { get; set; }
        public string description { get; set; }
        public DateTime created_at { get; set; }
        public int primary_language_id { get; set; }
    }
}