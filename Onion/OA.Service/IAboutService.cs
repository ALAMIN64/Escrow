using OA.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Service
{
   public interface IAboutService
    {
        IEnumerable<About> GetAbouts();
        About GetAbout(int id);
        void InsertAbout(About About);
        void UpdateAbout(About About);
        void DeleteAbout(int id);
    }
}
