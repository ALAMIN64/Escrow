using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OA.WEB.Models.ViewModel
{
    public class DeliveryWindow
    {
        public int DeliveryWindowID { get; set; }
        public string  Name { get; set; }
        public int  value { get; set; }
        public List<DeliveryWindow> GetWindow()
        {
            List<DeliveryWindow> types = new List<DeliveryWindow>();
            int i = 1;
            for(i = 1; i <= 15; i++)
            {
                var number = i.ToString();
                DeliveryWindow delivery = new DeliveryWindow();
                delivery.DeliveryWindowID = i;
                delivery.value = i;
                if (i == 1)
                {
                    delivery.Name = i+" "+ "Day";
                }
                else if(i==15)
                {
                    delivery.Name = "14 Days+";
                }
                else
                {
                    delivery.Name = i + " " + "Days";
                }
                types.Add(delivery);
            }
            return types;
        }
    }
}
