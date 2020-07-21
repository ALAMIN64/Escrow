using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OA.DATA.Entities;
using OA.Service;

namespace OA.WEB.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;
        private ITransactionService transactionService;
        private IPaymentService paymentService;

        public ManageController(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            ITransactionService transactionService,
            IPaymentService paymentService)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.transactionService = transactionService;
            this.paymentService = paymentService;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Dashboard()
        {
            DateTime dt = DateTime.Now.AddDays(7);
            ViewBag.Seller = userManager.Users.Where(s => s.UserTypeID == 2).Count();
            ViewBag.Employee = userManager.Users.Where(s => s.UserTypeID == 1).Count();
            ViewBag.allTransaction = transactionService.GetTransactions().Count();
            ViewBag.pendingTransaction = transactionService.GetTransactions().Where(s=>s.Status==1).Count();
            ViewBag.allPayment = paymentService.GetPayments().Count();
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Docs()
        {
           
            return View();
        }
    }
}
