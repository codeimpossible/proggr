using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Massive;

namespace Proggr.Models
{
    public class ModelBase : DynamicModel
    {

        public ModelBase() : base("MassiveHack")
        {

        }

        protected override string ConnectionString
        {
            get
            {
                return ConfigurationManager.AppSettings[ "SQLSERVER_CONNECTION_STRING" ];
            }
            set
            {
                // don't allow anyone to change this
            }
        }
    }
}