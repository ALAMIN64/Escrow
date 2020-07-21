using OA.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Service
{
   public interface IPolicyService
    {
        IEnumerable<Policy> GetPolicys();
        Policy GetPolicy(int id);
        void InsertPolicy(Policy Policy);
        void UpdatePolicy(Policy Policy);
        void DeletePolicy(int id);
    }
}
