using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OA.WEB.Models.AccountModels
{
    public class RecoverVM
    {
        public string Email { get; set; }
        public string UserID { get; set; }
        public string BaseCode { get; set; }
    }
}
