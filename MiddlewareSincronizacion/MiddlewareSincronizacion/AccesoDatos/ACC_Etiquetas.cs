using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class ACC_Etiquetas
    {
        #region Instancia
        private static ACC_Etiquetas instance = null;
        private static readonly object padlock = new object();

        public static ACC_Etiquetas ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new ACC_Etiquetas();
                }
                return instance;
            }
        }
        #endregion
        public string etiquetaTLP(List<getDatosEtiquetaPP2_Result> ee)
        {
            StringBuilder sb;

            sb = new StringBuilder();
            sb.AppendLine();

            if (ee[0].descripcion.Length < 74)
            {
                sb.AppendLine("N");

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A635,352,2,4,1,1,N,\"{0}\"",
                ee[0].puesto_trabajo));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A789,351,2,4,1,1,N,\"{0}\"",
                "Pto.Tbjo:"));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A554,325,2,4,1,1,N,\"{0}\"",
                "CANTIDAD:"));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A555,362,2,4,1,1,N,\"{0}\"",
                "FECHA:"));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A446,363,2,4,1,1,N,\"{0}\"",
                ee[0].fecha));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A260,363,2,4,1,1,N,\"{0}\"",
                "HORA:"));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A166,364,2,4,1,1,N,\"{0}\"",
                ee[0].hora));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A396,326,2,4,1,1,N,\"{0}\"",
                ee[0].cantidad));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A216,330,2,4,1,1,N,\"{0}\"",
                ee[0].unidad_medida));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A660,40,2,4,1,1,N,\"{0}\"",
                ee[0].cliente));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A793,290,2,4,1,1,N,\"{0}\"",
                ee[0].descripcion.Substring(0, ee[0].descripcion.Length)));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A793,262,2,4,1,1,N,\"{0}\"",
                ""));
                string cadena = ee[0].material + "-" + ee[0].lote + "-" + ee[0].cantidad;
                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "B721,228,2,1,2,6,107,B,\"{0}\"",
                cadena));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A795,77,2,4,1,1,N,\"{0}\"",
                "ORDEN:"));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A688,80,2,4,1,1,N,\"{0}\"",
                ee[0].num_orden));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A501,82,2,4,1,1,N,\"{0}\"",
                "LOTE:"));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A406,82,2,4,1,1,N,\"{0}\"",
                ee[0].lote));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A221,84,2,4,1,1,N,\"{0}\"",
                "A:"));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A173,85,2,4,1,1,N,\"{0}\"",
                ee[0].ancho));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A796,42,2,4,1,1,N,\"{0}\"",
                "CLIENTE:"));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A703,396,2,4,1,1,R,\"{0}\"",
                "GRUPO INDUSTRIAL GASAER DE RL DE CV"));

                sb.AppendLine("FE");
            }
            else
            {
                sb.AppendLine("N");

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A635,352,2,4,1,1,N,\"{0}\"",
                ee[0].puesto_trabajo));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A789,351,2,4,1,1,N,\"{0}\"",
                "Pto.Tbjo:"));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A554,325,2,4,1,1,N,\"{0}\"",
                "CANTIDAD:"));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A555,362,2,4,1,1,N,\"{0}\"",
                "FECHA:"));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A446,363,2,4,1,1,N,\"{0}\"",
                ee[0].fecha));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A260,363,2,4,1,1,N,\"{0}\"",
                "HORA:"));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A166,364,2,4,1,1,N,\"{0}\"",
                ee[0].hora));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A396,326,2,4,1,1,N,\"{0}\"",
                ee[0].cantidad));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A216,330,2,4,1,1,N,\"{0}\"",
                ee[0].unidad_medida));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A660,40,2,4,1,1,N,\"{0}\"",
                ee[0].cliente));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A793,290,2,4,1,1,N,\"{0}\"",
                ee[0].descripcion.Substring(0, 73)));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A793,262,2,4,1,1,N,\"{0}\"",
                ee[0].descripcion.Substring(73, ee[0].descripcion.Length)));
                string cadena = ee[0].material + "-" + ee[0].lote + "-" + ee[0].cantidad;
                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "B721,228,2,1,2,6,107,B,\"{0}\"",
                cadena));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A795,77,2,4,1,1,N,\"{0}\"",
                "ORDEN:"));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A688,80,2,4,1,1,N,\"{0}\"",
                ee[0].num_orden));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A501,82,2,4,1,1,N,\"{0}\"",
                "LOTE:"));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A406,82,2,4,1,1,N,\"{0}\"",
                ee[0].lote));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A221,84,2,4,1,1,N,\"{0}\"",
                "A:"));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A173,85,2,4,1,1,N,\"{0}\"",
                ee[0].ancho));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A796,42,2,4,1,1,N,\"{0}\"",
                "CLIENTE:"));

                sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "A703,396,2,4,1,1,R,\"{0}\"",
                "GRUPO INDUSTRIAL GASAER DE RL DE CV"));

                sb.AppendLine("FE");
            }
            sb.AppendLine("P1");
            return sb.ToString();
        }
        public string etiquetaGK(List<getDatosEtiquetaPP2_Result> ee)
        {
            if (ee[0].descripcion.Length < 74)
            {
                return "^XA\n" +
                    "^CI28\n" +
                    "^MMT\n" +
                    "^PW807\n" +
                    "^LL0408\n" +
                    "^LS0\n" +
                    "^FO704,320^GFA,01152,01152,00012,:Z64:\n" +
                    "eJzVk7Fr20AUxt9ZISoUZAcKCU2wMoYMJqPAIaf/ImNDxg6poUshhVNqSIeWLFkLMXQMmIyGM9ih/0C2rJdOpi0XeygIJPT6TndSl5QUOvXJoJ8/7t777jsE8A/Vood+sXk9UDGLzatRC89gCf6wtq69bsHuvbE/mQK0ifH8Yl/MAYKtAPS7Mz1TAP64zfDLBaakN0fP+eXnD+2U9vKsQHz9/pMg7mxrrQ9PP7YTgN0uLlD0xz7tjaL1u+/r/REQ70bhAsN++ob6dL5pPQxO1CvSw6wQ6J/MOXloZgW/9xIVJn+fCx/UGN7itDpxB7RyyKYgcsc+wFPpeIP+4tRyRNFVG3pMeTh3W9mAybxixTAvB1DMCrQu9WUQCgQ6xgEIO2CDodHLAWsNpeBIlxx510Yvh+0YPs4cL1QL7eBNGJr+5dFi6sOcHgNXnpxcWV4h/1Y3jQOtZxULxLTibCIrXsHfOi2v1/Oc1Xp3BHJk+wM1F24uxXlwVPo8AIbznnA+PXnFnX9Yxdy3frYozR8u0DXjMrE5PAFfTlQ3MbxE+s+ecLk19GzzZcke6TkUNvPEk2OWVvkjrtr8ycRwaOMx90jHdfcYwLGUiWUfONproVKBPa2pt86lqeZXdMvpY3lx03rki/mv6xduwusD:C017\n" +
                    "^FT695,344^A0I,28,28^FB108,1,0^FH\\^FDPto Tbjo:^FS\n" +
                    "^FT424,368^A0I,27,26^FB79,1,0^FH\\^FDFECHA:^FS\n" +
                    "^FT334,369^A0I,27,26^FB120,1,0^FH\\^FD" + ee[0].fecha + "^FS\n" +
                    "^FT198,370^A0I,27,26^FB70,1,0^FH\\^FDHORA:^FS\n" +
                    "^FT114,370^A0I,27,26^FB94,1,0^FH\\^FD" + ee[0].hora + "^FS\n" +
                    "^FT707,28^A0I,23,24^FB700,1,0^FH\\^FD" + ee[0].cliente + "^FS\n" +
                    "^BY2,3,99^FT744,139^BCI,,Y,N\n" +
                    "^FD>;" + ee[0].material + ">6-" + ee[0].lote + "->51>6" + ee[0].cantidad + "^FS\n" +
                    "^FT423,328^A0I,27,26^FB121,1,0^FH\\^FDCANTIDAD:^FS\n" +
                    "^FT291,328^A0I,27,26^FB126,1,0^FH\\^FD" + ee[0].cantidad + "^FS\n" +
                    "^FT797,75^A0I,28,28^FB94,1,0^FH\\^FDORDEN:^FS\n" +
                    "^FT692,75^A0I,28,28^FB141,1,0^FH\\^FD" + ee[0].num_orden + "^FS\n" +
                    "^FT515,75^A0I,28,28^FB70,1,0^FH\\^FDLOTE:^FS\n" +
                    "^FT429,75^A0I,28,28^FB157,1,0^FH\\^FD" + ee[0].lote + "^FS\n" +
                    "^FT209,75^A0I,28,28^FB25,1,0^FH\\^FDA:^FS\n" +
                    "^FT174,75^A0I,28,28^FB135,1,0^FH\\^FD" + ee[0].ancho + "^FS\n" +
                    "^FO449,335^GB126,0,7^FS\n" +
                    "^FO36,65^GB139,0,5^FS\n" +
                    "^FO272,64^GB157,0,5^FS\n" +
                    "^FO552,64^GB141,0,5^FS\n" +
                    "^FO54,317^GB99,0,4^FS\n" +
                    "^FO166,318^GB125,0,5^FS\n" +
                    "^FO15,359^GB101,0,6^FS\n" +
                    "^FO213,358^GB120,0,6^FS\n" +
                    "^FT573,347^A0I,28,28^FB120,1,0^FH\\^FD" + ee[0].puesto_trabajo + "^FS\n" +
                    "^FT150,327^A0I,27,26^FB91,1,0^FH\\^FD" + ee[0].unidad_medida + "^FS\n" +
                    "^FT797,28^A0I,21,21^FB81,1,0^FH\\^FDCLIENTE:^FS\n" +
                    "^FT799,253^A0I,21,21^FB793,1,0^FH^FD^FS\n" +
                    "^FT799,285^A0I,21,21^FB792,1,0^FH\\^FD" + ee[0].descripcion.Substring(0, ee[0].descripcion.Length).Replace("°", "_C2_B0") + "^FS\n" +
                    "^XZ";
            }
            else
            {
                return "^XA\n" +
                    "^CI28\n" +
                    "^MMT\n" +
                    "^PW807\n" +
                    "^LL0408\n" +
                    "^LS0\n" +
                    "^FO704,320^GFA,01152,01152,00012,:Z64:\n" +
                    "eJzVk7Fr20AUxt9ZISoUZAcKCU2wMoYMJqPAIaf/ImNDxg6poUshhVNqSIeWLFkLMXQMmIyGM9ih/0C2rJdOpi0XeygIJPT6TndSl5QUOvXJoJ8/7t777jsE8A/Vood+sXk9UDGLzatRC89gCf6wtq69bsHuvbE/mQK0ifH8Yl/MAYKtAPS7Mz1TAP64zfDLBaakN0fP+eXnD+2U9vKsQHz9/pMg7mxrrQ9PP7YTgN0uLlD0xz7tjaL1u+/r/REQ70bhAsN++ob6dL5pPQxO1CvSw6wQ6J/MOXloZgW/9xIVJn+fCx/UGN7itDpxB7RyyKYgcsc+wFPpeIP+4tRyRNFVG3pMeTh3W9mAybxixTAvB1DMCrQu9WUQCgQ6xgEIO2CDodHLAWsNpeBIlxx510Yvh+0YPs4cL1QL7eBNGJr+5dFi6sOcHgNXnpxcWV4h/1Y3jQOtZxULxLTibCIrXsHfOi2v1/Oc1Xp3BHJk+wM1F24uxXlwVPo8AIbznnA+PXnFnX9Yxdy3frYozR8u0DXjMrE5PAFfTlQ3MbxE+s+ecLk19GzzZcke6TkUNvPEk2OWVvkjrtr8ycRwaOMx90jHdfcYwLGUiWUfONproVKBPa2pt86lqeZXdMvpY3lx03rki/mv6xduwusD:C017\n" +
                    "^FT695,344^A0I,28,28^FB108,1,0^FH\\^FDPto Tbjo:^FS\n" +
                    "^FT424,368^A0I,27,26^FB79,1,0^FH\\^FDFECHA:^FS\n" +
                    "^FT334,369^A0I,27,26^FB120,1,0^FH\\^FD" + ee[0].fecha + "^FS\n" +
                    "^FT198,370^A0I,27,26^FB70,1,0^FH\\^FDHORA:^FS\n" +
                    "^FT114,370^A0I,27,26^FB94,1,0^FH\\^FD" + ee[0].hora + "^FS\n" +
                    "^FT707,28^A0I,23,24^FB700,1,0^FH\\^FD" + ee[0].cliente + "^FS\n" +
                    "^BY2,3,99^FT744,139^BCI,,Y,N\n" +
                    "^FD>;" + ee[0].material + ">6-" + ee[0].lote + "->51>6" + ee[0].cantidad + "^FS\n" +
                    "^FT423,328^A0I,27,26^FB121,1,0^FH\\^FDCANTIDAD:^FS\n" +
                    "^FT291,328^A0I,27,26^FB126,1,0^FH\\^FD" + ee[0].cantidad + "^FS\n" +
                    "^FT797,75^A0I,28,28^FB94,1,0^FH\\^FDORDEN:^FS\n" +
                    "^FT692,75^A0I,28,28^FB141,1,0^FH\\^FD" + ee[0].num_orden + "^FS\n" +
                    "^FT515,75^A0I,28,28^FB70,1,0^FH\\^FDLOTE:^FS\n" +
                    "^FT429,75^A0I,28,28^FB157,1,0^FH\\^FD" + ee[0].lote + "^FS\n" +
                    "^FT209,75^A0I,28,28^FB25,1,0^FH\\^FDA:^FS\n" +
                    "^FT174,75^A0I,28,28^FB135,1,0^FH\\^FD" + ee[0].ancho + "^FS\n" +
                    "^FO449,335^GB126,0,7^FS\n" +
                    "^FO36,65^GB139,0,5^FS\n" +
                    "^FO272,64^GB157,0,5^FS\n" +
                    "^FO552,64^GB141,0,5^FS\n" +
                    "^FO54,317^GB99,0,4^FS\n" +
                    "^FO166,318^GB125,0,5^FS\n" +
                    "^FO15,359^GB101,0,6^FS\n" +
                    "^FO213,358^GB120,0,6^FS\n" +
                    "^FT573,347^A0I,28,28^FB120,1,0^FH\\^FD" + ee[0].puesto_trabajo + "^FS\n" +
                    "^FT150,327^A0I,27,26^FB91,1,0^FH\\^FD" + ee[0].unidad_medida + "^FS\n" +
                    "^FT797,28^A0I,21,21^FB81,1,0^FH\\^FDCLIENTE:^FS\n" +
                    "^FT799,253^A0I,21,21^FB793,1,0^FH^FD" + ee[0].descripcion.Substring(73, ee[0].descripcion.Length).Replace("°", "_C2_B0") + "^FS\n" +
                    "^FT799,285^A0I,21,21^FB792,1,0^FH^FD" + ee[0].descripcion.Substring(0, 73).Replace("°", "_C2_B0") + "^FS\n" +
                    "^XZ";
            }
        }
    }
}
