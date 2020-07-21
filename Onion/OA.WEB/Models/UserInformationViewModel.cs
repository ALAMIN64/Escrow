using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OA.WEB.Models
{
    public class UserInformationViewModel
    {
        public int Id { get; set; }
        public string ApplicationUserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string ProfileImagePath { get; set; }
        public string FacebookUserName { get; set; }
    }
}
