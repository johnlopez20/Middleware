using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class ConexionSAP
    {
        public string ServerType { get; set; }
        public string System { get; set; }
        public string SystemNumber { get; set; }
        public string ApplicationServer { get; set; }
        public string Cliente { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Language { get; set; }
        public string SAPRouter { get; set; }
        public ConexionSAP()
        {
            ServerType = string.Empty;
            System = string.Empty;
            SystemNumber = string.Empty;
            ApplicationServer = string.Empty;
            Cliente = string.Empty;
            User = string.Empty;
            Password = string.Empty;
            SAPRouter = string.Empty;
            Language = string.Empty;
        }

    }
}
