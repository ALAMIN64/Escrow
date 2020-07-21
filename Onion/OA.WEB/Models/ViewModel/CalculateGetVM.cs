using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OA.WEB.Models.ViewModel
{
    public class CalculateGetVM
    {
        public string deliveryTypeName { get; set; }
        public decimal? deliveryFee { get; set; }
        public decimal amount { get; set; }
        public decimal escrowfee { get; set; }
        public decimal escrowfeePercentage { get; set; }
        public decimal total { get; set; }
    }

}
