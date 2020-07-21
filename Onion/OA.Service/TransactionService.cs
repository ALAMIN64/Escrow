using OA.DATA.Entities;
using OA.REPO;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transactions> TransactionRepository;
        private readonly IRepository<TransactionLink> transactionLinkRepository;

        public TransactionService(IRepository<Transactions> TransactionRepository,IRepository<TransactionLink> transactionLinkRepository)
        {
            this.TransactionRepository = TransactionRepository;
            this.transactionLinkRepository = transactionLinkRepository;
        }

        public void DeleteTransaction(int id)
        {
            Transactions Transaction = TransactionRepository.Get(id);
            TransactionRepository.Remove(Transaction);

        }

        public Transactions GetTransaction(int id)
        {
            return TransactionRepository.Get(id);
        }

        public IEnumerable<Transactions> GetTransactions()
        {
            return TransactionRepository.GetAll();
        }

        public void InsertTransaction(Transactions Transaction)
        {
            TransactionRepository.Insert(Transaction);
        }

        public void UpdateTransaction(Transactions Transaction)
        {
            TransactionRepository.Update(Transaction);
        }
    }
}
