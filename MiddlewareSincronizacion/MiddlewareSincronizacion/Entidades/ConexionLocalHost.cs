using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class ConexionLocalHost
    {
        public string Server { get; set; }
        public string DataBase { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public ConexionLocalHost()
        {
            Server = string.Empty;
            DataBase = string.Empty;
            User = string.Empty;
            Password = string.Empty;
        }
    }
}
