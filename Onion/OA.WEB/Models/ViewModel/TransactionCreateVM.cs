using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OA.WEB.Models.ViewModel
{
    public class TransactionCreateVM
    {
        public int TransactionLinkID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BuyerEmail { get; set; }
        public string ItemName { get; set; }
        public string DescriptionOfItem { get; set; }
        public string TermsOfReturns { get; set; }
        public string DeliveryName { get; set; }
        public decimal DeliveryFee { get; set; }
        public int? DeliveryTime { get; set; }
        public decimal Amount { get; set; }
        public decimal EscrowFee { get; set; }
        public decimal TotalFee { get; set; }
        public string SellerName { get; set; }
        public string SellerEmail { get; set; }
        public Guid LinkCode { get; set; }
        public string status { get; set; }
        public DateTime? startDate { get; set; }
    }
}
