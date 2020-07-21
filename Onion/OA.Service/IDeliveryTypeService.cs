using OA.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Service
{
    public interface IDeliveryTypeService
    {
        IEnumerable<DeliveryType> GetDeliveryTypes();
        DeliveryType GetDeliveryType(int id);
        void InsertDeliveryType(DeliveryType DeliveryType);
        void UpdateDeliveryType(DeliveryType DeliveryType);
        void DeleteDeliveryType(int id);
    }
}
