using OA.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Service
{
   public interface ITermsService
    {
        IEnumerable<Terms> GetTermss();
        Terms GetTerms(int id);
        void InsertTerms(Terms Terms);
        void UpdateTerms(Terms Terms);
        void DeleteTerms(int id);
    }
}
