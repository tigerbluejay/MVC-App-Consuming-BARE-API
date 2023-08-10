using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCAppConsumingBAREAPI.Models.IdentityDTOs
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }

        // i do not yet understand why we include this here
        // i will enable functionality if it is necessary
        // and explain here why UserDTO in the API project is different
        // from this one
        //public string Password { get; set; }
    }
}
