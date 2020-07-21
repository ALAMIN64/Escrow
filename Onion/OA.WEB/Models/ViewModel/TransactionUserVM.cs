using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace OA.WEB.Models.ViewModel
{
    public class TransactionUserVM
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ItemName { get; set; }
        public decimal Amount { get; set; }
        public string DescriptionOfItem { get; set; }
        public int DeliveryTypeID { get; set; }
        public string SellerName { get; set; }
        public string SellerEmail { get; set; }
        public string SellerID { get; set; }
        public string UserID { get; set; }
        public string UserTypeInTransaction { get; set; }
        public Guid LinkCode { get; set; }
        public string status { get; set; }
        public  DateTime? startDate { get; set; }

    }
}
