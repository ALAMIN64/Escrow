using OA.DATA.Entities;
using OA.REPO;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<Payment> PaymentRepository;
        public PaymentService(IRepository<Payment> PaymentRepository)
        {
            this.PaymentRepository = PaymentRepository;
        }

        public void DeletePayment(int id)
        {
            Payment Payment = PaymentRepository.Get(id);
            PaymentRepository.Remove(Payment);

        }

        public Payment GetPayment(int id)
        {
            return PaymentRepository.Get(id);
        }

        public IEnumerable<Payment> GetPayments()
        {
            return PaymentRepository.GetAll();
        }

        public void InsertPayment(Payment Payment)
        {
            PaymentRepository.Insert(Payment);
        }

        public void UpdatePayment(Payment Payment)
        {
            PaymentRepository.Update(Payment);
        }
    }
}
