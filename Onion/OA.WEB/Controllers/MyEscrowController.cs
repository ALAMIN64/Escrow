using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OA.DATA.Entities;
using OA.Service;
using OA.WEB.Models.ViewModel;
using RugerRumble.Services;

namespace OA.WEB.Controllers
{
    [Authorize]
    public class MyEscrowController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private ITransactionLinkService transactionLinkService;
        private ITransactionService transactionService;
        private IPaymentService paymentService;

        public MyEscrowController(UserManager<ApplicationUser> userManager, ITransactionService transactionService, ITransactionLinkService transactionLinkService, IPaymentService paymentService)
        {
            this.userManager = userManager;
            this.transactionLinkService = transactionLinkService;
            this.transactionService = transactionService;
            this.paymentService = paymentService;
        }
        public IActionResult Index()
        {
            List<TransactionUserVM> transactions = new List<TransactionUserVM>();
            var userID = GetCurrentUserAsync().Result.Id;
            var listtransactions = transactionService.GetTransactions().Where(s => s.UserID == userID).ToList();
            foreach (var item in listtransactions)
            {
                TransactionUserVM tu = new TransactionUserVM();
                tu.ID = item.Id;
                tu.ItemName = item.ItemName;
                tu.Amount = item.Amount;
                tu.UserTypeInTransaction = item.UserRoleId == 1 ? "Seller" : item.UserRoleId == 2 ? "Buyer" : "";
                tu.status = item.Status == 1 ? "<div style=\"color:red\"><strong>Pending</strong><div>" : item.Status == 2 ? "<div style=\"color:darkblue\"><strong>Paid</strong></div>" : item.Status == 3 ? "<div style=\"color:purple\"><strong>Shipped</strong></div>" : item.Status == 4 ? "<div style=\"color:green\"><strong>Received</strong></div>" : "";
                tu.startDate = item.AddDate;
                tu.Email = item.UserRoleId == 1 ? item.BuyerEmail : item.UserRoleId == 2 ? item.SellerEmail : "";
                transactions.Add(tu);
            }
            return View(transactions.OrderByDescending(s=>s.ID));
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);



        [HttpPost]
        public IActionResult UpdateStatus(TransactionDetails payment, [FromServices] IEmailSender emailSender, [FromServices] IConfiguration configuration)
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
            if (payment.statusID == 4)
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
                "<h3 style=\"color:forestgreen\">Pay Guardex</h3>" +
                "<div>" +
                    "<p>You have recieved product from " + tr.SellerEmail + "  </p>" +
                "</div>" +
                "<div>" +
                    "<p>Your have successfully recieved product.</p>" +
                    "<p>You can still view the transaction.</p>" +
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
                   "<p> " + tr.BuyerEmail + " recieved the product</p>" +
               "</div>" +
               "<div>" +
                   "<p>" + tr.BuyerEmail + " recieved the product successfully.</p>" +
                   "<p>When buyer recieve the product we will inform you.</p>" +
                   "<p>Thank you.</p>" +
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
