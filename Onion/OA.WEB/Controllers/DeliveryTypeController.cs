using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OA.Service;
using OA.WEB.Models.ViewModel;

namespace OA.WEB.Controllers
{
    public class DeliveryTypeController : Controller
    {
        private IDeliveryTypeService deliveryTypeService;

        public DeliveryTypeController(IDeliveryTypeService deliveryTypeService)
        {
            this.deliveryTypeService = deliveryTypeService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var types = deliveryTypeService.GetDeliveryTypes().ToList();
            List<DeliveryTypeVM> typeVM = new List<DeliveryTypeVM>();
            foreach(var item in types)
            {
                DeliveryTypeVM dT = new DeliveryTypeVM();
                dT.ID = item.Id;
                dT.Name = item.DeliveryTypeName;
                typeVM.Add(dT);
            }
            return View(typeVM);
        }
    }
}
