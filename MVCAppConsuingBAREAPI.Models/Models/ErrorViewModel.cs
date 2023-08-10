using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCAppConsumingBAREAPI.Models.Models
{
    // this class and its properties are generated automatically when you select MVC Project Template
    // this class also exists in the MVCAppConsumingBAREAPI.Web.Models namespace
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        // if the RequestId is null, return false
        // if the RequestId is not null, return true
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        
        // which is the same as writing
        //public bool ShowRequestId
        //{
        //    get
        //    {
        //        return !string.IsNullOrEmpty(RequestId);
        //    }
        //}
    }
}
