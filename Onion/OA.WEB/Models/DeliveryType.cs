using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OA.WEB.Models
{
    public class DeliveryTypes
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<DeliveryTypes> deliveryTypes()
        {
            List<DeliveryTypes> types = new List<DeliveryTypes>();
            DeliveryTypes type1 = new DeliveryTypes();
            DeliveryTypes type2 = new DeliveryTypes();
            type1.ID = 1;
            type2.ID = 2;
            type1.Name = "Free Delivery";
            type2.Name = "Paid Delivery";
            types.Add(type1);
            types.Add(type2);
            return types;
        }
    }
   
        



}
