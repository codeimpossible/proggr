using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proggr.Configuration {
    public interface ConfigurationSettings {
        string OAuthClientSecret { get; }
        string OAuthClientKey { get; }
        string DatabaseConnectionString { get; }
    }
}
