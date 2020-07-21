using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OA.DATA.Entities;
using OA.REPO.Migrations;
using OA.Service;
using OA.WEB.Models.ViewModel;
using RugerRumble.Services;

namespace OA.WEB.Controllers
{
    [Authorize]
    public class UtilityController : Controller
    {
        private IAboutService aboutService;
        private ITermsService termsService;
        private IPolicyService policyService;
        private IFeeService feeService;
        private IContactService contactService;
        private IHomeService homeService;

        public UtilityController(IAboutService aboutService, ITermsService termsService, IPolicyService policyService,IFeeService feeService,IContactService contactService,IHomeService homeService)
        {
            this.aboutService = aboutService;
            this.termsService = termsService;
            this.policyService = policyService;
            this.feeService = feeService;
            this.contactService = contactService;
            this.homeService = homeService;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult About()
        {
            List<AboutVM> abouts = new List<AboutVM>();
            var aboutList = aboutService.GetAbouts().ToList();
            foreach (var item in aboutList)
            {
                AboutVM about = new AboutVM();
                about.ID = item.Id;
                about.Title = item.Title;
                about.Description = item.Description;
                abouts.Add(about);
            }
            return View(abouts);
        }
        [HttpPost]
        public ActionResult AddAbout(IFormCollection form)
        {
            AboutVM about = JsonConvert.DeserializeObject<AboutVM>(form["aboutInfo"]);

            try
            {
                About data = new About();
                data.Title = about.Title;
                data.Description = about.Description;
                data.AddDate = DateTime.Now;
                data.Status = 1;
                aboutService.InsertAbout(data);
                return Json(new { success = true });

            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAbout(int id)
        {
            About about = new About();
            about = aboutService.GetAbout(id);

            var JSON = Json(new { about = about, success = true });
            return JSON;
        }
        [HttpPost]
        public ActionResult UpdateAbout(IFormCollection form)
        {
            About about = JsonConvert.DeserializeObject<About>(form["aboutInfo"]);
            var aboutDB = aboutService.GetAbout(about.Id);
            try
            {
                if (aboutDB.Id > 0)
                {
                    aboutDB.Title = about.Title;
                    aboutDB.Description = about.Description;
                    aboutDB.UpdateDate = DateTime.Now;
                    aboutService.UpdateAbout(aboutDB);
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
        public ActionResult DeleteAbout(int id)
        {
            try
            {
                aboutService.DeleteAbout(id);
                var JSON = Json(new { success = true });
                return JSON;
            }
            catch (Exception ex)
            {
                var JSON = Json(new { success = false });

                return JSON;
            }

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Terms()
        {
            List<TermsVM> terms = new List<TermsVM>();
            var termsList = termsService.GetTermss().ToList();
            foreach (var item in termsList)
            {
                TermsVM term = new TermsVM();
                term.ID = item.Id;
                term.Title = item.Title;
                term.Description = item.Description;
                terms.Add(term);
            }
            return View(terms);
        }
        [HttpPost]
        public ActionResult AddTerms(IFormCollection form)
        {
            TermsVM terms = JsonConvert.DeserializeObject<TermsVM>(form["termsInfo"]);

            try
            {
                Terms data = new Terms();
                data.Title = terms.Title;
                data.Description = terms.Description;
                data.AddDate = DateTime.Now;
                data.Status = 1;
                termsService.InsertTerms(data);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTerms(int id)
        {
            Terms terms = new Terms();
            terms = termsService.GetTerms(id);

            var JSON = Json(new { terms = terms, success = true });
            return JSON;
        }
        [HttpPost]
        public ActionResult UpdateTerms(IFormCollection form)
        {
            Terms terms = JsonConvert.DeserializeObject<Terms>(form["termsInfo"]);
            var termsDB = termsService.GetTerms(terms.Id);
            try
            {
                if (termsDB.Id > 0)
                {
                    termsDB.Title = terms.Title;
                    termsDB.Description = terms.Description;
                    termsDB.UpdateDate = DateTime.Now;
                    termsService.UpdateTerms(termsDB);
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
        public ActionResult DeleteTerms(int id)
        {
            try
            {
                termsService.DeleteTerms(id);
                var JSON = Json(new { success = true });
                return JSON;
            }
            catch (Exception ex)
            {
                var JSON = Json(new { success = false });

                return JSON;
            }

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Policy()
        {
            List<PolicyVM> policy = new List<PolicyVM>();
            var policyList = policyService.GetPolicys().ToList();
            foreach (var item in policyList)
            {
                PolicyVM term = new PolicyVM();
                term.ID = item.Id;
                term.Title = item.Title;
                term.Description = item.Description;
                policy.Add(term);
            }
            return View(policy);
        }
        [HttpPost]
        public ActionResult Addpolicy(IFormCollection form)
        {
            PolicyVM policy = JsonConvert.DeserializeObject<PolicyVM>(form["policyInfo"]);

            try
            {
                Policy data = new Policy();
                data.Title = policy.Title;
                data.Description = policy.Description;
                data.AddDate = DateTime.Now;
                data.Status = 1;
                policyService.InsertPolicy(data);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editpolicy(int id)
        {
            Policy policy = new Policy();
            policy = policyService.GetPolicy(id);

            var JSON = Json(new { policy = policy, success = true });
            return JSON;
        }
        [HttpPost]
        public ActionResult Updatepolicy(IFormCollection form)
        {
            Policy policy = JsonConvert.DeserializeObject<Policy>(form["policyInfo"]);
            var policyDB = policyService.GetPolicy(policy.Id);
            try
            {
                if (policyDB.Id > 0)
                {
                    policyDB.Title = policy.Title;
                    policyDB.Description = policy.Description;
                    policyDB.UpdateDate = DateTime.Now;
                    policyService.UpdatePolicy(policyDB);
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
        public ActionResult Deletepolicy(int id)
        {
            try
            {
                policyService.DeletePolicy(id);
                var JSON = Json(new { success = true });
                return JSON;
            }
            catch (Exception ex)
            {
                var JSON = Json(new { success = false });

                return JSON;
            }

        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Contacts()
        {
            List<ContactVM> fees = new List<ContactVM>();
            var feeList = contactService.GetContacts().ToList();
            foreach (var item in feeList)
            {
                ContactVM term = new ContactVM();
                term.ID = item.Id;
                term.FullName = item.FullName;
                term.Email = item.Email;
                term.Description = item.Description;
                term.Title = item.Title;
                fees.Add(term);
            }
            return View(fees);
        }
   
        [HttpPost]
        public ActionResult ResponseContact(IFormCollection form, [FromServices] IEmailSender emailSender, [FromServices] IConfiguration configuration)
        {
            TermsVM policy = JsonConvert.DeserializeObject<TermsVM>(form["TermsInfo"]);
            var policyDB = contactService.GetContact(policy.ID);
            try
            {
                var title = policy.Title;
                var description = policy.Description;
                emailSender.Post(
                          subject: title,
                          body: description,
                          recipients: policyDB.Email,
                          sender: configuration["AdminContact"]);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteContact(int id)
        {
            try
            {
                contactService.DeleteContact(id);
                var JSON = Json(new { success = true });
                return JSON;
            }
            catch (Exception ex)
            {
                var JSON = Json(new { success = false });

                return JSON;
            }
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult FeeCalcucate()
        {
            List<FeeCalculateVM> fees = new List<FeeCalculateVM>();
            var feeList = feeService.GetFees().ToList();
            foreach (var item in feeList)
            {
                FeeCalculateVM term = new FeeCalculateVM();
                term.Id = item.Id;
                term.Amount1 = item.Amount1;
                term.Amount2 = item.Amount2;
                term.Percentage = item.Percentage;
                term.TotalFees = item.TotalFees;
                fees.Add(term);
            }
            return View(fees);
        }
        [HttpPost]
        public ActionResult AddFeeCalcucate(IFormCollection form)
        {
            FeeCalculateVM fee = JsonConvert.DeserializeObject<FeeCalculateVM>(form["feeInfo"]);

            try
            {
                FeePerAmount data = new FeePerAmount();
                data.Amount1 = fee.Amount1;
                data.Amount2 = fee.Amount2;
                data.Percentage = fee.Percentage;
                data.TotalFees = fee.TotalFees;
                data.AddDate = DateTime.Now;
                data.Status = 1;
                feeService.InsertFee(data);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFeeCalcucate(int id)
        {
            FeePerAmount fee = new FeePerAmount();
            fee = feeService.GetFee(id);

            var JSON = Json(new { fee = fee, success = true });
            return JSON;
        }
        [HttpPost]
        public ActionResult UpdateFeeCalcucate(IFormCollection form)
        {
            FeePerAmount fee = JsonConvert.DeserializeObject<FeePerAmount>(form["feeInfo"]);
            var feeDB = feeService.GetFee(fee.Id);
            try
            {
                if (feeDB.Id > 0)
                {
                    feeDB.Amount1 = fee.Amount1;
                    feeDB.Amount2 = fee.Amount2;
                    feeDB.Percentage = fee.Percentage;
                    feeDB.TotalFees = fee.TotalFees;
                    feeDB.UpdateDate = DateTime.Now;
                    feeService.UpdateFee(feeDB);
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
        public ActionResult DeleteFeeCalcucate(int id)
        {
            try
            {
                feeService.DeleteFee(id);
                var JSON = Json(new { success = true });
                return JSON;
            }
            catch (Exception ex)
            {
                var JSON = Json(new { success = false });

                return JSON;
            }

        }




        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult HomeContent()
        {
            List<HomeVM> abouts = new List<HomeVM>();
            var aboutList = homeService.GetHomeContents().ToList();
            foreach (var item in aboutList)
            {
                HomeVM about = new HomeVM();
                about.ID = item.Id;
                about.Title = item.Title;
                about.Desciption = item.Desciption;
                abouts.Add(about);
            }
            return View(abouts);
        }
        [HttpPost]
        public ActionResult AddHomeContent(IFormCollection form)
        {
            AboutVM about = JsonConvert.DeserializeObject<AboutVM>(form["aboutInfo"]);

            try
            {
                HomeContent data = new HomeContent();
                data.Title = about.Title;
                data.Desciption = about.Description;
                data.AddDate = DateTime.Now;
                data.Status = 1;
                homeService.InsertHomeContent(data);
                return Json(new { success = true });

            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditHomeContent(int id)
        {
            HomeContent about = new HomeContent();
            about = homeService.GetHomeContent(id);

            var JSON = Json(new { about = about, success = true });
            return JSON;
        }
        [HttpPost]
        public ActionResult UpdateHomeContent(IFormCollection form)
        {
            HomeContent about = JsonConvert.DeserializeObject<HomeContent>(form["aboutInfo"]);
            var aboutDB = homeService.GetHomeContent(about.Id);
            try
            {
                if (aboutDB.Id > 0)
                {
                    aboutDB.Title = about.Title;
                    aboutDB.Desciption = about.Desciption;
                    aboutDB.UpdateDate = DateTime.Now;
                    homeService.UpdateHomeContent(aboutDB);
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
        public ActionResult DeleteHomeContent(int id)
        {
            try
            {
                homeService.DeleteHomeContent(id);
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
