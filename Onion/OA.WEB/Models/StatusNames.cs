using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OA.WEB.Models
{
    public class StatusNames
    {
        public int Status { get; set; }
        public string Name { get; set; }
        public List<StatusNames> GetStatuses()
        {
            List<StatusNames> types = new List<StatusNames>();
            StatusNames type1 = new StatusNames();
            StatusNames type2 = new StatusNames();
            StatusNames type3 = new StatusNames();
            StatusNames type4 = new StatusNames();
            type1.Status = 1;
            type2.Status = 2;
            type3.Status = 3;
            type4.Status = 4;
            type1.Name = "Pending";
            type2.Name = "Paid";
            type3.Name = "Shipped";
            type4.Name = "Recieved";
            types.Add(type1);
            types.Add(type2);
            types.Add(type3);
            types.Add(type4);
            return types;
        }
    }
}
