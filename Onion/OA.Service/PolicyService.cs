using OA.DATA.Entities;
using OA.REPO;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Service
{
    public class PolicyService : IPolicyService
    {
        private readonly IRepository<Policy> PolicyRepository;
        public PolicyService(IRepository<Policy> PolicyRepository)
        {
            this.PolicyRepository = PolicyRepository;
        }

        public void DeletePolicy(int id)
        {
            Policy Policy = PolicyRepository.Get(id);
            PolicyRepository.Remove(Policy);

        }

        public Policy GetPolicy(int id)
        {
            return PolicyRepository.Get(id);
        }

        public IEnumerable<Policy> GetPolicys()
        {
            return PolicyRepository.GetAll();
        }

        public void InsertPolicy(Policy Policy)
        {
            PolicyRepository.Insert(Policy);
        }

        public void UpdatePolicy(Policy Policy)
        {
            PolicyRepository.Update(Policy);
        }
    }
}
