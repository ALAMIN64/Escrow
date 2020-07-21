using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace OA.WEB.Models.ViewModel
{
    public class PaymentVM
    {
        public int ID { get; set; }
        public string DocumentsUrl { get; set; }
        public string DocumentsUrl2 { get; set; }
        public string DocumentsUrl3 { get; set; }
        public int LinkID { get; set; }
        public decimal Amount { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }
}
