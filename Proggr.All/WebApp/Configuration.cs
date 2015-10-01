using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace WebApp
{
    public class Configuration
    {
        private const string ConfigurationFileName = "C:\\proggr\\.proggrrc";
        private static ConfigurationData _data;

        public string GetClientSecret()
        {
            LoadConfigurationData();
            return _data.ClientSecret;
        }

        public string GetClientId()
        {
            LoadConfigurationData();
            return _data.ClientId;
        }

        private void LoadConfigurationData()
        {
            if (_data == null)
            {
                object objLock;
                lock (objLock = new object())
                {
                    if (_data == null)
                    {
                        var json = File.ReadAllText(ConfigurationFileName);
                        _data = JsonConvert.DeserializeObject<ConfigurationData>(json);
                    }
                }
            }
        }

        private class ConfigurationData
        {
            public string ClientSecret { get; set; }
            public string ClientId { get; set; }
        }
    }
}