using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion
{
    public class ConfigSAP
    {
        public string systemSAP { get; set; }
        public string systemNumberSAP { get; set; }
        public string applicationServer { get; set; }
        public string client { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string language { get; set; }
        public string SAProuter { get; set; }
        public ConfigSAP()
        {
            systemSAP = string.Empty;
            systemNumberSAP = string.Empty;
            applicationServer = string.Empty;
            client = string.Empty;
            user = string.Empty;
            password = string.Empty;
            language = string.Empty;
            SAProuter = string.Empty;
        }
    }
}
