using System;
using System.Collections.Generic;
using System.Text;

namespace OA.DATA.Entities
{
    public class Payment : BaseEntity
    {
        public string DocumentsUrl { get; set; }
        public string DocumentsUrl2 { get; set; }
        public string DocumentsUrl3 { get; set; }
        public int LinkID { get; set; }
        public decimal Amount { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string Description { get; set; }
    }
}
