using OA.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Service
{
    public interface IPaymentService
    {
        IEnumerable<Payment> GetPayments();
        Payment GetPayment(int id);
        void InsertPayment(Payment Payment);
        void UpdatePayment(Payment Payment);
        void DeletePayment(int id);
    }
}
