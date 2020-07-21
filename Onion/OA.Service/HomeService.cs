using OA.DATA.Entities;
using OA.REPO;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Service
{
    public class HomeService : IHomeService
    {
        private readonly IRepository<HomeContent> HomeContentRepository;
        public HomeService(IRepository<HomeContent> HomeContentRepository)
        {
            this.HomeContentRepository = HomeContentRepository;
        }

        public void DeleteHomeContent(int id)
        {
            HomeContent HomeContent = HomeContentRepository.Get(id);
            HomeContentRepository.Remove(HomeContent);
        }

       

        public HomeContent GetHomeContent(int id)
        {
            return HomeContentRepository.Get(id);
        }

        public IEnumerable<HomeContent> GetHomeContents()
        {
            return HomeContentRepository.GetAll();
        }

       

        public void InsertHomeContent(HomeContent HomeContent)
        {
            HomeContentRepository.Insert(HomeContent);
        }

     

        public void UpdateHomeContent(HomeContent HomeContent)
        {
            HomeContentRepository.Update(HomeContent);
        }

     
    }
}
