using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OA.WEB.Models.ViewModel
{
    public class FeeCalculateVM
    {
        public int Id { get; set; }
        public decimal Amount1 { get; set; }
        public decimal Amount2 { get; set; }
        public decimal Percentage { get; set; }
        public decimal TotalFees { get; set; }
    }
}
