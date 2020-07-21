using OA.DATA.Entities;
using OA.REPO;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Service
{
    public class TransactionLinkService : ITransactionLinkService
    {
        private readonly IRepository<TransactionLink> TransactionLinkRepository;
        public TransactionLinkService(IRepository<TransactionLink> TransactionLinkRepository)
        {
            this.TransactionLinkRepository = TransactionLinkRepository;
        }

        public void DeleteTransactionLink(int id)
        {
            TransactionLink TransactionLink = TransactionLinkRepository.Get(id);
            TransactionLinkRepository.Remove(TransactionLink);

        }

        public TransactionLink GetTransactionLink(int id)
        {
            return TransactionLinkRepository.Get(id);
        }

        public IEnumerable<TransactionLink> GetTransactionLinks()
        {
            return TransactionLinkRepository.GetAll();
        }

        public void InsertTransactionLink(TransactionLink TransactionLink)
        {
            TransactionLinkRepository.Insert(TransactionLink);
        }

        public void UpdateTransactionLink(TransactionLink TransactionLink)
        {
            TransactionLinkRepository.Update(TransactionLink);
        }
    }
}
