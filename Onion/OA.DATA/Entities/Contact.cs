using System;
using System.Collections.Generic;
using System.Text;

namespace OA.DATA.Entities
{
   public class Contact:BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
