using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OA.WEB.Models
{
    public class BuyerTransactionVM
    {
        public int TransactionID { get; set; }
        public string SellerEmail { get; set; }
        public string GenCode { get; set; }
        public string ItemName { get; set; }
        public decimal Amount { get; set; }
        public string DescriptionOfItem { get; set; }
        public string DeliveryType { get; set; }
        public decimal? Deliveryfee { get; set; }
        public string TermsOfReturns { get; set; }
        public string ProductImageUrl { get; set; }
        public string ProductImageUrl1 { get; set; }
        public string ProductImageUrl2 { get; set; }
        public string ProductImageUrl3 { get; set; }
        public string ProductImageUrl4 { get; set; }
        public string UserRoleId { get; set; }
        public string UserID { get; set; }
        public string Status { get; set; }
    }
}
