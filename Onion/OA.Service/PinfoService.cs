using OA.DATA.Entities;
using OA.REPO;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Service
{
    public class PinfoService : IPinfoService
    {
        private readonly IRepository<Pinfo> PinfoRepository;
        public PinfoService(IRepository<Pinfo> PinfoRepository)
        {
            this.PinfoRepository = PinfoRepository;
        }

        public void DeletePinfo(int id)
        {
            Pinfo Pinfo = PinfoRepository.Get(id);
            PinfoRepository.Remove(Pinfo);

        }

        public Pinfo GetPinfo(int id)
        {
            return PinfoRepository.Get(id);
        }
        public IEnumerable<Pinfo> GetPinfos()
        {
            return PinfoRepository.GetAll();
        }

        public void InsertPinfo(Pinfo Pinfo)
        {
            PinfoRepository.Insert(Pinfo);
        }

        public void UpdatePinfo(Pinfo Pinfo)
        {
            PinfoRepository.Update(Pinfo);
        }
    }
}
