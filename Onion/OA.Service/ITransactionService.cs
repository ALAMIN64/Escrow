using OA.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Service
{
    public interface ITransactionService
    {
        IEnumerable<Transactions> GetTransactions();
        Transactions GetTransaction(int id);
        void InsertTransaction(Transactions Transaction);
        void UpdateTransaction(Transactions Transaction);
        void DeleteTransaction(int id);
    }
}
