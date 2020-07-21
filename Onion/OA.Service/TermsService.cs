using OA.DATA.Entities;
using OA.REPO;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Service
{
    public class TermsService : ITermsService
    {
        private readonly IRepository<Terms> TermsRepository;
        public TermsService(IRepository<Terms> TermsRepository)
        {
            this.TermsRepository = TermsRepository;
        }

        public void DeleteTerms(int id)
        {
            Terms Terms = TermsRepository.Get(id);
            TermsRepository.Remove(Terms);

        }

        public Terms GetTerms(int id)
        {
            return TermsRepository.Get(id);
        }

        public IEnumerable<Terms> GetTermss()
        {
            return TermsRepository.GetAll();
        }

        public void InsertTerms(Terms Terms)
        {
            TermsRepository.Insert(Terms);
        }

        public void UpdateTerms(Terms Terms)
        {
            TermsRepository.Update(Terms);
        }
    }
}
