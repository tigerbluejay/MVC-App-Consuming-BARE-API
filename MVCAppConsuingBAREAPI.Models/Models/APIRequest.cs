using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MVCAppConsumingBAREAPI.Utilities.StaticDetails;

namespace MVCAppConsumingBAREAPI.Models.Models
{
    // API Request does not exist in the API Project, this is because
    // we are sending the Request from the MVC application. This replaces the
    // Swagger UI call and fields.
    public class APIRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string Token { get; set; }
    }
}
