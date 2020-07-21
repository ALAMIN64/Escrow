using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.DATA.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? AddDate { get; set; }
        public int? UserTypeID { get; set; }
        public int Status { get; set; }
    }
}
