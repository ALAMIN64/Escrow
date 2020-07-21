using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OA.DATA.Entities;
using OA.REPO.Migrations;
using OA.Service;
using OA.WEB.Models;
using OA.WEB.Models.ViewModel;

namespace OA.WEB.Controllers
{
    public class HomeController : Controller
    {
        private IAboutService aboutService;
        private ITermsService termsService;
        private IPolicyService policyService;
        private IFeeService feeService;
        private IContactService contactService;
        private IPinfoService pinfoService;
        private IHomeService homeService;

        public HomeController(IAboutService aboutService, ITermsService termsService, IPolicyService policyService, IFeeService feeService, IContactService contactService,IPinfoService pinfoService,IHomeService homeService)
        {
            this.aboutService = aboutService;
            this.termsService = termsService;
            this.policyService = policyService;
            this.feeService = feeService;
            this.contactService = contactService;
            this.pinfoService = pinfoService;
            this.homeService = homeService;
        }
        public IActionResult Index()
        {
            var homes = homeService.GetHomeContents();
            return View(homes);
        }
        [HttpGet]
        public IActionResult About()
        {
            AboutVM about = new AboutVM();
            var abt = aboutService.GetAbouts().FirstOrDefault();
            if (abt != null)
            {
                about.Title = abt.Title;
                about.Description = abt.Description;
                return View(about);
            }
            else
            {
                return View(about);
            }

        }
        [HttpGet]
        public IActionResult Privacy()
        {
            PolicyVM about = new PolicyVM();
            var abt = policyService.GetPolicys().FirstOrDefault();
            if (abt != null)
            {
                about.Title = abt.Title;
                about.Description = abt.Description;
                return View(about);
            }
            else
            {
                return View(about);
            }
        }
        [HttpGet]
        public IActionResult Terms()
        {
            TermsVM about = new TermsVM();
            var abt = termsService.GetTermss().FirstOrDefault();
            if (abt != null)
            {
                about.Title = abt.Title;
                about.Description = abt.Description;
                return View(about);
            }
            else
            {
                return View(about);
            }
        }
        [HttpGet]
        public IActionResult FeeAndPolicy()
        {
            List<FeeCalculateVM> fees = new List<FeeCalculateVM>();
            var abt = feeService.GetFees().ToList();
            foreach (var item in abt)
            {
                FeeCalculateVM fee = new FeeCalculateVM();
                fee.Id = item.Id;
                fee.Amount1 = item.Amount1;
                fee.Amount2 = item.Amount2;
                fee.Percentage = item.Percentage;
                fee.TotalFees = item.TotalFees;
                fees.Add(fee);
            }
            return View(fees);
        }

        [HttpGet]
        public IActionResult Contacts()
        {
            ContactVM contact = new ContactVM();
            return View(contact);
        }
        [HttpPost]
        public IActionResult RCVContact(ContactVM contact)
        {
            Contact db = new Contact();
            db.FullName = contact.FullName;
            db.Email = contact.Email;
            db.Description = contact.Description;
            db.Title = contact.Title;
            db.Status = 1;
            db.AddDate = DateTime.Now;
            contactService.InsertContact(db);
            contact.ID = db.Id;
            return View(nameof(Contacts), contact);
        }
        [HttpGet]
        public IActionResult Process()
        {
            var lstinfo = pinfoService.GetPinfos().FirstOrDefault();
            return View(lstinfo);
        }
    }
}
