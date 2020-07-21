using OA.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Service
{
   public interface IPinfoService
    {
        IEnumerable<Pinfo> GetPinfos();
        Pinfo GetPinfo(int id);
        void InsertPinfo(Pinfo Pinfo);
        void UpdatePinfo(Pinfo Pinfo);
        void DeletePinfo(int id);
    }
}
