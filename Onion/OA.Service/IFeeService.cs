using OA.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Service
{
   public interface IFeeService
    {
        IEnumerable<FeePerAmount> GetFees();
        FeePerAmount GetFee(int id);
        void InsertFee(FeePerAmount Fee);
        void UpdateFee(FeePerAmount Fee);
        void DeleteFee(int id);
    }
}
