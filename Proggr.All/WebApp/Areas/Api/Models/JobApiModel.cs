using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Areas.Api.Models
{
    public class JobApiModel
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public string Arguments { get; set; }
        public string JobType { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateCompleted { get; set; }
        public string CompletedBy { get; set; }
        public string CompletedArguments { get; set; }
    }
}