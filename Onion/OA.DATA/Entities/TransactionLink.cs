using System;
using System.Collections.Generic;
using System.Text;

namespace OA.DATA.Entities
{
    public class TransactionLink : BaseEntity
    {
        public string SellerID { get; set; }
        public string BuyerID { get; set; }
        public int TransactionID { get; set; }
        public Guid LinkCode { get; set; }

    }
}
