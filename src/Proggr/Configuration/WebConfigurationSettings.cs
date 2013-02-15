using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Proggr.Configuration {
    public class WebConfigurationSettings : ConfigurationSettings {

        public string OAuthClientSecret {
            get { return ConfigurationManager.AppSettings[ "github.oauth.secret" ]; }
        }

        public string OAuthClientKey {
            get { return ConfigurationManager.AppSettings[ "github.oauth.clientkey" ]; }
        }

        public string DatabaseConnectionString {
            get { return ConfigurationManager.AppSettings[ "SQLSERVER_CONNECTION_STRING" ]; }
        }
    }
}