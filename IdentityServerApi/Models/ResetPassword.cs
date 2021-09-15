using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerApi.Models
{
    public class ResetPassword
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
