using System;
using System.Collections.Generic;
using System.Text;

namespace OA.DATA.Entities
{
   public class FeePerAmount:BaseEntity
    {
        public decimal Amount1 { get; set; }
        public decimal Amount2 { get; set; }
        public decimal Percentage { get; set; }
        public decimal TotalFees { get; set; }

    }
}
