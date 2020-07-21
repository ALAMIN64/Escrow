using OA.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Service
{
    public interface ITransactionLinkService
    {
        IEnumerable<TransactionLink> GetTransactionLinks();
        TransactionLink GetTransactionLink(int id);
        void InsertTransactionLink(TransactionLink TransactionLink);
        void UpdateTransactionLink(TransactionLink TransactionLink);
        void DeleteTransactionLink(int id);
    }
}
