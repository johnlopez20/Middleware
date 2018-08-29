using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion
{
    public partial class samEntities:DbContext
    {
        public samEntities(string connection)
            : base(connection)
        {

        }

        internal void INSERT_usuarios_MDL(string p1, string p2, string p3, string p4, string p5, string p6, string p7, int p8, int p9, string p10, string p11, string p12, string p13, string p14, string p15, string p16, int p17, int p18, string p19, string p20, string p21, string p22, string p23, string p24, string p25)
        {
            throw new NotImplementedException();
        }
    }
}
