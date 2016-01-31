using System;
using System.Collections.Generic;
using System.Runtime.Hosting;
using Newtonsoft.Json;

namespace Worker.Models
{
    public class JobDescriptor
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public string Arguments { get; set; }
        public string JobType { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public T GetArgumentsJson<T>()
        {
            return String.IsNullOrWhiteSpace(Arguments) ? default(T) : JsonConvert.DeserializeObject<T>(Arguments);
        } 
    }
}
