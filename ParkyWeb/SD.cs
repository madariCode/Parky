using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyWeb
{
    public static class SD
    {
        public static string APIUrlBase = "https://localhost:44345/";
        public static string ParquesNacionalesAPIPath = APIUrlBase + "api/v1/parquesnacionales/";
        public static string SenderosAPIPath = APIUrlBase + "api/v1/senderos/";
        public static string CuentaAPIPath = APIUrlBase + "api/v1/usuarios/";
    }
}
