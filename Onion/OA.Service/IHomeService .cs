using OA.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Service
{
   public interface IHomeService
    {
        IEnumerable<HomeContent> GetHomeContents();
        HomeContent GetHomeContent(int id);
        void InsertHomeContent(HomeContent HomeContent);
        void UpdateHomeContent(HomeContent HomeContent);
        void DeleteHomeContent(int id);
    }
}
