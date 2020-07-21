using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OA.WEB.Models.ViewModel
{
    public class TransactionDetails
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BuyerEmail { get; set; }
        public string ItemName { get; set; }
        public decimal Amount { get; set; }
        public string DescriptionOfItem { get; set; }
        public string DeliveryType { get; set; }
        public decimal? Deliveryfee { get; set; }
        public int? DeliveryTime { get; set; }
        public string TermsOfReturns { get; set; }
        public string ProductImageUrl { get; set; }
        public string ProductImageUrl1 { get; set; }
        public string ProductImageUrl2 { get; set; }
        public string ProductImageUrl3 { get; set; }
        public string ProductImageUrl4 { get; set; }
        public string SellerName { get; set; }
        public string SellerEmail { get; set; }
        public decimal escrowfee { get; set; }
        public decimal Totalfee { get; set; }
        public int TransactionID { get; set; }
        public Guid LinkCode { get; set; }
        public string Status { get; set; }
        public int statusID { get; set; }
        public int LinkID { get; set; }

    }
}
