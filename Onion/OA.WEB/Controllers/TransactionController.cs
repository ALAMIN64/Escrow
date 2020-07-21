using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OA.DATA.Entities;
using OA.REPO.Migrations;
using OA.Service;
using OA.WEB.Models;
using OA.WEB.Models.ViewModel;
using System.Drawing;
using System.Linq.Expressions;
using System.Transactions;
using RugerRumble.Services;
using Microsoft.Extensions.Configuration;

namespace OA.WEB.Controllers
{
    public class TransactionController : Controller
    {
        private IDeliveryTypeService deliveryTypeService;
        private ITransactionService transactionService;
        private UserManager<ApplicationUser> userManager;
        private IWebHostEnvironment env;
        private ITransactionLinkService transactionLinkService;
        private IFeeService feeService;
        private SignInManager<ApplicationUser> signInManager;
        private readonly IPaymentService paymentService;

        public TransactionController(IWebHostEnvironment env, IDeliveryTypeService deliveryTypeService, ITransactionService transactionService, UserManager<ApplicationUser> userManager, ITransactionLinkService transactionLinkService, IFeeService feeService, IPaymentService paymentService)
        {
            this.deliveryTypeService = deliveryTypeService;
            this.transactionService = transactionService;
            this.userManager = userManager;
            this.env = env;
            this.transactionLinkService = transactionLinkService;
            this.feeService = feeService;
            this.paymentService = paymentService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            DeliveryTypes delivery = new DeliveryTypes();
            ViewBag.DeliveryType = new SelectList(delivery.deliveryTypes(), "ID", "Name");
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            DeliveryTypes delivery = new DeliveryTypes();
            ViewBag.DeliveryType = new SelectList(delivery.deliveryTypes(), "ID", "Name");
            UserTypeNames names = new UserTypeNames();
            ViewBag.UserRoleName = new SelectList(names.GetTypeNames(), "TypeID", "TypeName");
            DeliveryWindow window = new DeliveryWindow();
            ViewBag.DeliveryWindow = new SelectList(window.GetWindow(), "DeliveryWindowID", "Name");
            return View();
        }
        private async Task<ApplicationUser> GetCurrentUserAsync() => await userManager.GetUserAsync(HttpContext.User);

        [HttpPost]
        public IActionResult GenerateLink(IFormCollection form, [FromServices] IEmailSender emailSender, [FromServices] IConfiguration configuration)
        {
            var users = GetCurrentUserAsync();
            //transactiona.SellerID = users.Result.Id;
            TransactionVM transaction = JsonConvert.DeserializeObject<TransactionVM>(form["EmployeeInfo"]);
            try
            {
                Transactions transactions = new Transactions();
                if (transaction.UserRoleId == 1)
                {
                    transactions.SellerName = users.Result.FirstName + " " + users.Result.LastName;
                    transactions.SellerEmail = users.Result.Email;
                    transactions.SellerID = users.Result.Id;
                    transactions.BuyerEmail = transaction.Email;
                }
                if (transaction.UserRoleId == 2)
                {
                    transactions.SellerEmail = transaction.SellerEmail;
                    transactions.BuyerEmail = users.Result.Email;
                }
                transactions.UserID = users.Result.Id;
                transactions.FirstName = transaction.FirstName;
                transactions.LastName = transaction.LastName;
                transactions.ItemName = transaction.ItemName;
                transactions.Amount = transaction.Amount;
                transactions.UserRoleId = transaction.UserRoleId;
                transactions.DescriptionOfItem = transaction.DescriptionOfItem;
                transactions.DeliveryTypeID = transaction.DeliveryTypeID;
                transactions.Deliveryfee = transaction.Deliveryfee;
                transactions.DeliveryWindowID = transaction.DeliveryWindowID;
                transactions.DeliveryTime = transaction.DeliveryTime;
                transactions.TotalDeliveryTime = transaction.DeliveryWindowID == 15 ? transaction.DeliveryTime + 14 : transaction.DeliveryWindowID + transaction.DeliveryTime;
                transactions.TermsOfReturns = transaction.TermsOfReturns;
                transactions.AddDate = DateTime.Now;
                transactions.Status = 1;
                int i = 0;
                var uploadFiles = Request.Form.Files;
                foreach (var file in uploadFiles)
                {
                    Guid nameguid = Guid.NewGuid();
                    string webrootpath = env.WebRootPath;
                    string filename = nameguid.ToString();
                    string extension = Path.GetExtension(file.FileName);
                    string foldername = "ProductImage";
                    filename = filename + extension;
                    string path = Path.Combine(webrootpath, foldername, filename);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    string pathName = Path.Combine(foldername, filename);
                    if (i == 0)
                    {
                        transactions.ProductImageUrl = "~/ProductImage/" + filename;
                    }
                    if (i == 1)
                    {
                        transactions.ProductImageUrl1 = "~/ProductImage/" + filename;
                    }
                    if (i == 2)
                    {
                        transactions.ProductImageUrl2 = "~/ProductImage/" + filename;
                    }
                    if (i == 3)
                    {
                        transactions.ProductImageUrl3 = "~/ProductImage/" + filename;
                    }
                    if (i == 4)
                    {
                        transactions.ProductImageUrl4 = "~/ProductImage/" + filename;
                    }
                    i++;
                }
                int feeID = GetFeeIDByAmount(transaction.Amount);
                var efp = feeService.GetFee(feeID).Percentage;
                var ef = transactions.escrowfee = (transaction.Amount * efp) / 100;
                if (transaction.Deliveryfee != null)
                {
                    transactions.Totalfee = transaction.Amount + transaction.Deliveryfee.Value + ef;
                }
                else if (transaction.Deliveryfee == null)
                {
                    transactions.Totalfee = transaction.Amount + ef;

                }
                transactionService.InsertTransaction(transactions);
                TransactionLink link = new TransactionLink();
                Guid guid = Guid.NewGuid();
                link.SellerID = transactions.SellerID;
                link.TransactionID = transactions.Id;
                link.LinkCode = guid;
                link.AddDate = DateTime.Now;
                link.Status = 1;
                transactionLinkService.InsertTransactionLink(link);
                string coded = link.LinkCode.ToString();
                var linkas = Url.Action("TransactionView", "MyTransaction", new { code = link.LinkCode }, Request.Scheme, Request.Host.ToString());


                var title = "Pay Guardex: You have a transaction.";
                var title1 = "Pay Guardex: You have started a transaction.";
               ///mail to buyer:
                    var mailbody = "<html>" +
                    "<head>" +
                    "<link rel=\"stylesheet\" href=\"https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css\" integrity=\"sha384-9aIt2nRpC12Uk9gS9baDl411NQApFmC26EwAOH8WgZl5MYYxFfc+NcPb1dKGj7Sk\" crossorigin=\"anonymous\">" +
                    "</head>" +
                    "<body>" +
                    "<div class=\"container\">" +
                    "<div class=\"row\">" +
                    "<div class=\"col-md-12\" style=\"padding:30px;background-color:#d8e9ff\">" +
                    "<h3 style=\"color:forestgreen\">You have a transaction on Pay Guardex</h3>" +
                    "<div>" +
                        "<p>You have transaction with " + transactions.SellerEmail + " </p>" +
                    "</div>" +
                    "<div>" +
                        "<p>" + transactions.SellerEmail + " has created a transaction with you on payguardex.com.Your transaction code is :<br><div style=\"text-align:center\"><h5> <b> " + coded + "</b></h5></div></p>" +
                        "<p>Use the code to manage transaction and here is the bank information for your payment.</p>" +
                        "<p>Bank informations goes here</p>" +
                        "<p>Please visit the link or click the button to provide us payment documents.</p>" +
                        "<p>" + linkas + "</p>" +
                    "</div>" +
                    "<div style=\"text-align:center\">" +
                        "<a href=" + linkas + " class=\"btn btn-success\"> Manage Transaction : </a>" +
                     "</div>" +
                     "</div>" +
                     "</div>" +
                  "</div>" +
                  "</ body >" +
                  "</html>";
                ///mail to seller
                    var mailbody1 = "<html>" +
                   "<head>" +
                   "<link rel=\"stylesheet\" href=\"https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css\" integrity=\"sha384-9aIt2nRpC12Uk9gS9baDl411NQApFmC26EwAOH8WgZl5MYYxFfc+NcPb1dKGj7Sk\" crossorigin=\"anonymous\">" +
                   "</head>" +
                   "<body>" +
                   "<div class=\"container\">" +
                   "<div class=\"row\">" +
                   "<div class=\"col-md-12\" style=\"padding:30px;background-color:#d8e9ff\">" +
                   "<h3 style=\"color:forestgreen\">You have started a transaction on Pay Guardex</h3>" +
                   "<div>" +
                       "<p>You started have transaction with " + transactions.BuyerEmail + " </p>" +
                   "</div>" +
                   "<div>" +
                       "<p>Your transaction code is : " + coded + "</p>" +
                       "<p>Please do not share this code to anyone.</p>" +
                       "<p>We have sent an email to buyer.</p>" +
                       "<p></p>" +
                   "</div>" +
                   "<div style=\"text-align:center\">" +
                       "<a href=" + linkas + " class=\"btn btn-success\"> Manage Transaction</a>" +
                    "</div>" +
                    "</div>" +
                    "</div>" +
                 "</div>" +
                 "</ body >" +
                 "</html>";

                 emailSender.Post(
                           subject: title,
                           body: mailbody,
                           recipients: transactions.BuyerEmail,
                           sender: configuration["AdminContact"]);
                 emailSender.Post(
                           subject: title1,
                           body: mailbody1,
                           recipients: transactions.SellerEmail,
                           sender: configuration["AdminContact"]);
                return Json(new { success = true, link = coded });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        public IActionResult ViewDetails(int id)
        {
            StatusNames statusNames = new StatusNames();
            ViewBag.StatusEs = new SelectList(statusNames.GetStatuses().Where(s => s.Status != 1), "Status", "Name");
            TransactionDetails details = new TransactionDetails();
            Transactions tr = transactionService.GetTransaction(id);
            TransactionLink link = transactionLinkService.GetTransactionLinks().Where(s => s.TransactionID == id).FirstOrDefault();
            details.FirstName = tr.FirstName;
            details.LastName = tr.LastName;
            details.BuyerEmail = tr.BuyerEmail;
            details.ItemName = tr.ItemName;
            details.DescriptionOfItem = tr.DescriptionOfItem;
            details.Amount = tr.Amount;
            details.DeliveryType = tr.DeliveryTypeID == 1 ? "Free Delivery" : tr.DeliveryTypeID == 2 ? "Paid Delivery" : "";
            details.Deliveryfee = tr.Deliveryfee != 0 ? tr.Deliveryfee : tr.Deliveryfee == 0 ? 0 : 0;
            details.DeliveryTime = tr.DeliveryTime;
            details.TermsOfReturns = tr.TermsOfReturns;
            details.ProductImageUrl = tr.ProductImageUrl;
            details.ProductImageUrl1 = tr.ProductImageUrl1;
            details.ProductImageUrl2 = tr.ProductImageUrl2;
            details.ProductImageUrl3 = tr.ProductImageUrl3;
            details.ProductImageUrl4 = tr.ProductImageUrl4;
            details.SellerName = tr.SellerName;
            details.SellerEmail = tr.SellerEmail;
            details.escrowfee = tr.escrowfee;
            details.Totalfee = tr.Totalfee;
            details.LinkCode = link.LinkCode;
            details.LinkID = link.Id;
            details.TransactionID = tr.Id;
            details.statusID = tr.Status;
            details.Status = tr.Status == 1 ? "<div style=\"color:red\">Pending<div>" : tr.Status == 2 ? "<div style=\"color:yellow\">Paid</div>" : tr.Status == 3 ? "<div style=\"color:purple\">Shipped</div>" : tr.Status == 4 ? "<div style=\"color:green\">Receieved</div>" : "";

            return View(details);
        }

        [HttpPost]
        public ActionResult CalculateFee(IFormCollection form)
        {
            CalculatePostVM about = JsonConvert.DeserializeObject<CalculatePostVM>(form["modelInfo"]);
            try
            {
                CalculateGetVM calculate = new CalculateGetVM();
                var tam = calculate.amount = about.Amount;
                calculate.deliveryFee = about.DeliveryFee;
                int feeID = GetFeeIDByAmount(about.Amount);
                var efp = calculate.escrowfeePercentage = feeService.GetFee(feeID).Percentage;
                var ef = calculate.escrowfee = (tam * efp) / 100;
                if (calculate.deliveryFee != null)
                {
                    calculate.total = tam + calculate.deliveryFee.Value + ef;
                }
                else if (calculate.deliveryFee == null)
                {
                    calculate.total = tam + ef;

                }
                return Json(new { success = true, calculate = calculate });

            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }

        }
        [HttpPost]
        public int GetFeeIDByAmount(decimal am)
        {
            var feepercentage = feeService.GetFees().ToList();
            int count = 0;
            int feeeid = 0;
            foreach (var item in feepercentage)
            {
                if (am > item.Amount1 && am < item.Amount2)
                {
                    count = count + 1;
                    var feelist = feeService.GetFees().Where(s => s.Amount1 == item.Amount1 && s.Amount2 == item.Amount2).ToList().FirstOrDefault();
                    feeeid = feelist.Id;
                }
            }
            var aa = feeeid;
            return aa;
        }

        [HttpPost]
        public IActionResult UpdateStatus(TransactionDetails payment,[FromServices] IEmailSender emailSender, [FromServices] IConfiguration configuration)
        {
            var tr = transactionService.GetTransaction(payment.TransactionID);
            var link = transactionLinkService.GetTransactionLinks().Where(s => s.TransactionID == tr.Id).FirstOrDefault();
            var payements = paymentService.GetPayments().Where(s => s.LinkID == link.Id).FirstOrDefault();
            link.Status = payment.statusID;
            tr.Status = payment.statusID;
            payements.Status = payment.statusID;
            paymentService.UpdatePayment(payements);
            transactionService.UpdateTransaction(tr);
            transactionLinkService.UpdateTransactionLink(link);
            if (payment.statusID == 3)
            {
                var linkas = Url.Action("TransactionView", "MyTransaction", new { code = link.LinkCode }, Request.Scheme, Request.Host.ToString());
                string coded = link.LinkCode.ToString();
                var title = "Pay Guardex: You have a transaction.";
                var title1 = "Pay Guardex: You have started a transaction.";
                var mailbody = "<html>" +
                "<head>" +
                "<link rel=\"stylesheet\" href=\"https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css\" integrity=\"sha384-9aIt2nRpC12Uk9gS9baDl411NQApFmC26EwAOH8WgZl5MYYxFfc+NcPb1dKGj7Sk\" crossorigin=\"anonymous\">" +
                "</head>" +
                "<body>" +
                "<div class=\"container\">" +
                "<div class=\"row\">" +
                "<div class=\"col-md-12\" style=\"padding:30px;background-color:#d8e9ff\">" +
                "<h3 style=\"color:forestgreen\">You have a transaction on Pay Guardex</h3>" +
                "<div>" +
                    "<p>" + tr.SellerEmail + " shipped the product </p>" +
                "</div>" +
                "<div>" +
                    "<p>" + tr.SellerEmail + "has marked the product as shipped when you recieve the product please let us know.</p>" +
                    "<p>Please visit the link or click the button to view the transaction.</p>" +
                    "<p>" + linkas + "</p>" +
                "</div>" +
                "<div style=\"text-align:center\">" +
                    "<a href=" + linkas + " class=\"btn btn-success\"> Manage Transaction : </a>" +
                 "</div>" +
                 "</div>" +
                 "</div>" +
              "</div>" +
              "</ body >" +
              "</html>";
                var mailbody1 = "<html>" +
               "<head>" +
               "<link rel=\"stylesheet\" href=\"https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css\" integrity=\"sha384-9aIt2nRpC12Uk9gS9baDl411NQApFmC26EwAOH8WgZl5MYYxFfc+NcPb1dKGj7Sk\" crossorigin=\"anonymous\">" +
               "</head>" +
               "<body>" +
               "<div class=\"container\">" +
               "<div class=\"row\">" +
               "<div class=\"col-md-12\" style=\"padding:30px;background-color:#d8e9ff\">" +
               "<h3 style=\"color:forestgreen\">Pay Guardex</h3>" +
               "<div>" +
                   "<p>You have shipped the product for " + tr.BuyerEmail + " </p>" +
               "</div>" +
               "<div>" +
                   "<p>Your transaction code is : " + coded + "</p>" +
                   "<p>When buyer recieve the product we will inform you.</p>" +
                   "<p>We have sent an email to buyer.</p>" +
                   "<p></p>" +
               "</div>" +
               "<div style=\"text-align:center\">" +
                "</div>" +
                "</div>" +
                "</div>" +
             "</div>" +
             "</ body >" +
             "</html>";
                emailSender.Post(
                          subject: title,
                          body: mailbody,
                          recipients: tr.BuyerEmail,
                          sender: configuration["AdminContact"]);
                emailSender.Post(
                       subject: title1,
                       body: mailbody1,
                       recipients: tr.SellerEmail,
                       sender: configuration["AdminContact"]);
            }
            return Redirect("/Myescrow/Index");
        }
    }
}
