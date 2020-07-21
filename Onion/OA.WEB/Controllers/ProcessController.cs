using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OA.DATA.Entities;
using OA.Service;
using OA.WEB.Models.ViewModel;

namespace OA.WEB.Controllers
{
    [Authorize]
    public class ProcessController : Controller
    {
        private IPinfoService pinfoService;

        public ProcessController(IPinfoService pinfoService)
        {
            this.pinfoService = pinfoService;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult page()
        {
            List<PinfoVM> infors = new List<PinfoVM>();
            var inforList = pinfoService.GetPinfos().ToList();
            foreach (var item in inforList)
            {
                PinfoVM infor = new PinfoVM();
                infor.Id = item.Id;
                infor.Step1 = item.Step1;
                infor.Step2 = item.Step2;
                infor.Step3 = item.Step3;
                infor.Step4 = item.Step4;
                infor.Step5 = item.Step5;
                infor.Step6 = item.Step6;
                infors.Add(infor);
            }
            return View(infors);
        }
        [HttpPost]
        public ActionResult Addinfor(IFormCollection form)
        {
            PinfoVM infor = JsonConvert.DeserializeObject<PinfoVM>(form["inforInfo"]);

            try
            {
                Pinfo data = new Pinfo();
                data.Step1 = infor.Step1;
                data.Step2 = infor.Step2;
                data.Step3 = infor.Step3;
                data.Step4 = infor.Step4;
                data.Step5 = infor.Step5;
                data.Step6 = infor.Step6;
                data.AddDate = DateTime.Now;
                data.Status = 1;
                pinfoService.InsertPinfo(data);
                return Json(new { success = true });

            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editinfor(int id)
        {
            Pinfo infor = new Pinfo();
            infor = pinfoService.GetPinfo(id);

            var JSON = Json(new { infor = infor, success = true });
            return JSON;
        }
        [HttpPost]
        public ActionResult Updateinfor(IFormCollection form)
        {
            Pinfo infor = JsonConvert.DeserializeObject<Pinfo>(form["pinfo"]);
            var inforDB = pinfoService.GetPinfo(infor.Id);
            try
            {
                if (inforDB.Id > 0)
                {
                    inforDB.Step1 = infor.Step1;
                    inforDB.Step2 = infor.Step2;
                    inforDB.Step3 = infor.Step3;
                    inforDB.Step4 = infor.Step4;
                    inforDB.Step5 = infor.Step5;
                    inforDB.Step6 = infor.Step6;
                    inforDB.UpdateDate = DateTime.Now;
                    pinfoService.UpdatePinfo(inforDB);
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deleteinfor(int id)
        {
            try
            {
                pinfoService.DeletePinfo(id);
                var JSON = Json(new { success = true });
                return JSON;
            }
            catch (Exception ex)
            {
                var JSON = Json(new { success = false });

                return JSON;
            }

        }

    }
}
