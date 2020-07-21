using OA.DATA.Entities;
using OA.REPO;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Service
{
    public class AboutService : IAboutService
    {
        private readonly IRepository<About> AboutRepository;
        public AboutService(IRepository<About> AboutRepository)
        {
            this.AboutRepository = AboutRepository;
        }

        public void DeleteAbout(int id)
        {
            About About = AboutRepository.Get(id);
            AboutRepository.Remove(About);

        }

        public About GetAbout(int id)
        {
            return AboutRepository.Get(id);
        }

        public IEnumerable<About> GetAbouts()
        {
            return AboutRepository.GetAll();
        }

        public void InsertAbout(About About)
        {
            AboutRepository.Insert(About);
        }

        public void UpdateAbout(About About)
        {
            AboutRepository.Update(About);
        }
    }
}
