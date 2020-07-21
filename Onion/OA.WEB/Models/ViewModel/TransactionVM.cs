using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OA.WEB.Models.ViewModel
{
    public class TransactionVM
    {
        public string BuyerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
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
        public string ProductImageUrl2 { get; set; }
        public string ProductImageUrl3 { get; set; }
        public string ProductImageUrl4 { get; set; }
        public string SellerName { get; set; }
        public string SellerEmail { get; set; }
        public string SellerID { get; set; }
        public int UserRoleId { get; set; }
        public string UserID { get; set; }
        public int ID { get; set; }
    }
}
