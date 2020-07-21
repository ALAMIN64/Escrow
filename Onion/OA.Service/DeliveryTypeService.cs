using OA.DATA.Entities;
using OA.REPO;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Service
{
    public class DeliveryTypeService : IDeliveryTypeService
    {
        private readonly IRepository<DeliveryType> DeliveryTypeRepository;
        public DeliveryTypeService(IRepository<DeliveryType> DeliveryTypeRepository)
        {
            this.DeliveryTypeRepository = DeliveryTypeRepository;
        }

        public void DeleteDeliveryType(int id)
        {
            DeliveryType DeliveryType = DeliveryTypeRepository.Get(id);
            DeliveryTypeRepository.Remove(DeliveryType);

        }

        public DeliveryType GetDeliveryType(int id)
        {
            return DeliveryTypeRepository.Get(id);
        }

        public IEnumerable<DeliveryType> GetDeliveryTypes()
        {
            return DeliveryTypeRepository.GetAll();
        }

        public void InsertDeliveryType(DeliveryType DeliveryType)
        {
            DeliveryTypeRepository.Insert(DeliveryType);
        }

        public void UpdateDeliveryType(DeliveryType DeliveryType)
        {
            DeliveryTypeRepository.Update(DeliveryType);
        }
    }
}
