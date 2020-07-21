using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using OA.DATA.Entities;
using OA.REPO.Migrations;
using OA.Service;
using OA.WEB.Models;
using OA.WEB.Models.ViewModel;
using RugerRumble.Services;

namespace OA.WEB.Controllers
{
    [Authorize]
    public class ManagePaymentController : Controller
    {
        private ITransactionService transactionService;
        private ITransactionLinkService transactionLinkService;
        private IPaymentService paymentService;

        public ManagePaymentController(ITransactionService transactionService,
            ITransactionLinkService transactionLinkService, IPaymentService paymentService)
        {
            this.transactionService = transactionService;
            this.transactionLinkService = transactionLinkService;
            this.paymentService = paymentService;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            StatusNames statusNames = new StatusNames();
            ViewBag.StatusEs = new SelectList(statusNames.GetStatuses(), "Status", "Name");
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetAllPayments()
        {
            JsonResult result;
            try
            {
                int statusddl = 0;
                var statusID = HttpContext.Request.Form["statusID"];

                string search = HttpContext.Request.Form["search[value]"][0];
                string draw = HttpContext.Request.Form["draw"][0];
                string order = HttpContext.Request.Form["order[0][column]"][0];
                string orderDir = HttpContext.Request.Form["order[0][dir]"][0];
                int startRec = Convert.ToInt32(HttpContext.Request.Form["start"][0]);
                int pageSize = Convert.ToInt32(HttpContext.Request.Form["length"][0]);
                int totalRecords = 0;
                if (!string.IsNullOrEmpty(statusID))
                {
                    statusddl = int.Parse(statusID);
                }
                var firstPartOfQuery = paymentService.GetPayments().AsQueryable();
                int ifSearch = 0;
                var secondPartOfQuery = firstPartOfQuery;
                List<PaymentVM> data = new List<PaymentVM>();
                if (statusddl != 0)
                {
                    secondPartOfQuery = firstPartOfQuery.Where(s => s.Status == statusddl);
                }

                if (secondPartOfQuery.Count() > 0)
                {
                    totalRecords = secondPartOfQuery.AsEnumerable().Count();
                    data = secondPartOfQuery.AsEnumerable().Skip(startRec).Take(pageSize).Select(

                            s => new PaymentVM
                            {
                                ID = s.Id,
                                LinkID = s.LinkID,
                                Amount = s.Amount,
                                AccountNumber = s.AccountNumber,
                                Description = s.Description,
                                BankName = s.BankName,
                            })
                        .ToList();
                }

                data = this.SortByColumnWithOrder(order, orderDir, data);
                int recFilter = (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search)) ? ifSearch : firstPartOfQuery.AsEnumerable().Count();

                result = this.Json(new
                {
                    draw = Convert.ToInt32(draw),
                    recordsTotal = totalRecords,
                    recordsFiltered = recFilter,
                    data = data
                });

                return result;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return Json(new
                {
                    draw = 0,
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    data = new List<UserVM>()
                });
            }
        }

        private List<PaymentVM> SortByColumnWithOrder(string order, string orderDir, List<PaymentVM> data)
        {
            List<PaymentVM> lst = new List<PaymentVM>();
            try
            {
                switch (order)
                {

                    case "0":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.ID).ToList() : data.OrderBy(p => p.ID).ToList();
                        break;
                    case "1":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.LinkID).ToList() : data.OrderBy(p => p.LinkID).ToList();
                        break;
                    case "2":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Amount).ToList() : data.OrderBy(p => p.Amount).ToList();
                        break;
                    case "3":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.AccountNumber).ToList() : data.OrderBy(p => p.AccountNumber).ToList();
                        break;
                    case "4":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.BankName).ToList() : data.OrderBy(p => p.BankName).ToList();
                        break;

                    default:
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.ID).ToList() : data.OrderBy(p => p.ID).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {

                Console.Write(ex);
            }
            return lst;
        }

        [HttpPost]
        public IActionResult UpdateStatus(Payment payment, [FromServices] IEmailSender emailSender, [FromServices] IConfiguration configuration)
        {
            var payements = paymentService.GetPayment(payment.Id);
            var link = transactionLinkService.GetTransactionLink(payements.LinkID);
            var tr = transactionService.GetTransaction(link.TransactionID);
            link.Status = payment.Status;
            tr.Status = payment.Status;
            payements.Status = payment.Status;
            paymentService.UpdatePayment(payements);
            transactionService.UpdateTransaction(tr);
            transactionLinkService.UpdateTransactionLink(link);
            if (payment.Status == 2)
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
                "<h3 style=\"color:forestgreen\">Recieved your payment</h3>" +
                "<div>" +
                    "<p>We have recieved you payment successfully. </p>" +
                "</div>" +
                "<div>" +
                    "<p> We have notify " + tr.SellerEmail + " for shipping the product.</p>" +
                    "<p>Please visit the link or click the button to view the transaction.</p>" +
                    "<p>" + linkas + "</p>" +
                "</div>" +
                "<div style=\"text-align:center\">" +
                    "<a href=" + linkas + " class=\"btn btn-success\"> Manage Transaction </a>" +
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
               "<h3 style=\"color:forestgreen\">Payment has been recieved.</h3>" +
               "<div>" +
                   "<p>Please ship the product for " + tr.BuyerEmail + " </p>" +
               "</div>" +
               "<div>" +
                   "<p>Your transaction code is : " + coded + "</p>" +
                   "<p>We have recieved payment please ship the product and then marked the status as shipped.</p>" +
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
            return Redirect("/ManagePayment/Index");

        }

        private void SentMailUpdate(Transactions tr, TransactionLink link)
        {



        }

        [HttpGet]
        public IActionResult ViewDetails(int id)
        {
            StatusNames statusNames = new StatusNames();
            ViewBag.StatusEs = new SelectList(statusNames.GetStatuses(), "Status", "Name");
            var payement = paymentService.GetPayment(id);
            return View(payement);
        }
    }
}
