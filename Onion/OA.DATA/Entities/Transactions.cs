using System;
using System.Collections.Generic;
using System.Text;

namespace OA.DATA.Entities
{
    public class Transactions : BaseEntity
    {
        public string BuyerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BuyerEmail { get; set; }
        public string ItemName { get; set; }
        public decimal Amount { get; set; }
        public string DescriptionOfItem { get; set; }
        public int DeliveryTypeID { get; set; }
        public decimal? Deliveryfee { get; set; }
        public int? DeliveryTime { get; set; }
        public int? DeliveryWindowID { get; set; }
        public int? TotalDeliveryTime { get; set; }
        public string TermsOfReturns { get; set; }
        public string ProductImageUrl { get; set; }
        public string ProductImageUrl1 { get; set; }
        public string ProductImageUrl2{ get; set; }
        public string ProductImageUrl3 { get; set; }
        public string ProductImageUrl4 { get; set; }
        public string SellerName { get; set; }
        public string SellerEmail { get; set; }
        public string SellerID { get; set; }
        public int UserRoleId { get; set; }
        public string UserID { get; set; }
        public decimal escrowfee { get; set; }
        public decimal Totalfee { get; set; }



    }
}
