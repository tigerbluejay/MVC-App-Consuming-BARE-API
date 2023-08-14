using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCAppConsumingBAREAPI.Utilities
{
    public static class StaticDetails
    {
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        public static string SessionToken = "JWTToken";
    }
}