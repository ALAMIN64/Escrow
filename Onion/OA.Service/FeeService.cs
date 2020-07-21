using OA.DATA.Entities;
using OA.REPO;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Service
{
    public class FeeService : IFeeService
    {
        private readonly IRepository<FeePerAmount> FeeRepository;
        public FeeService(IRepository<FeePerAmount> FeeRepository)
        {
            this.FeeRepository = FeeRepository;
        }

        public void DeleteFee(int id)
        {
            FeePerAmount Fee = FeeRepository.Get(id);
            FeeRepository.Remove(Fee);

        }

        public FeePerAmount GetFee(int id)
        {
            return FeeRepository.Get(id);
        }
        public IEnumerable<FeePerAmount> GetFees()
        {
            return FeeRepository.GetAll();
        }

        public void InsertFee(FeePerAmount Fee)
        {
            FeeRepository.Insert(Fee);
        }

        public void UpdateFee(FeePerAmount Fee)
        {
            FeeRepository.Update(Fee);
        }
    }
}
