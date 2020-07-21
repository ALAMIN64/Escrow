using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OA.WEB.Models
{
    public class UserTypeNames
    {
        public int TypeID { get; set; }
        public string TypeName { get; set; }
        public List<UserTypeNames> GetTypeNames()
        {
            List<UserTypeNames> types = new List<UserTypeNames>();
            UserTypeNames type1 = new UserTypeNames();
            UserTypeNames type2 = new UserTypeNames();
            type1.TypeID = 1;
            type2.TypeID = 2;
            type1.TypeName = "Seller";
            type2.TypeName = "Buyer";
            types.Add(type1);
            types.Add(type2);
            return types;
        }
    }
}
