using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OA.Service;
using OA.WEB.Models;
using OA.WEB.Models.ViewModel;
using RugerRumble.Services;

namespace OA.WEB.Controllers
{
    public class MyTransactionController : Controller
    {
        private ITransactionLinkService transactionLinkService;
        private ITransactionService transactionService;
        private IFeeService feeService;

        public MyTransactionController(ITransactionLinkService transactionLinkService, ITransactionService transactionService, IFeeService feeService)
        {
            this.transactionLinkService = transactionLinkService;
            this.transactionService = transactionService;
            this.feeService = feeService;
        }
        [HttpGet]
        public IActionResult TransactionView(Guid code, FindModel model)
        {
            if (model != null)
            {
                code = model.Code;
            }
            StatusNames statusNames = new StatusNames();
            ViewBag.StatusEs = new SelectList(statusNames.GetStatuses().Where(s => s.Status != 1 && s.Status != 2), "Status", "Name");
            decimal df = 0;
            var link = transactionLinkService.GetTransactionLinks().Where(s => s.LinkCode == code).FirstOrDefault();
            var tr = transactionService.GetTransaction(link.TransactionID);
            TransactionDetails details = new TransactionDetails();
            details.TransactionID = tr.Id;
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
            details.statusID = tr.Status;
            details.Status = tr.Status == 1 ? "<div style=\"color:red\">Pending<div>" : tr.Status == 2 ? "<div style=\"color:yellow\">Paid</div>" : tr.Status == 3 ? "<div style=\"color:purple\">Shipped</div>" : tr.Status == 4 ? "<div style=\"color:green\">Receieved</div>" : "";

            return View(details);
        }
        [HttpPost]
        public IActionResult UpdateStatus(IFormCollection form, [FromServices] IEmailSender emailSender, [FromServices] IConfiguration configuration)
        {
            StatusUpdateModel details = JsonConvert.DeserializeObject<StatusUpdateModel>(form["aboutInfo"]);
            try
            {
                var transaction = transactionService.GetTransaction(details.ID);
                var link = transactionLinkService.GetTransactionLinks().Where(s => s.TransactionID == transaction.Id).FirstOrDefault();
                transaction.Status = details.Status.Value;
                link.Status = details.Status.Value;
                transactionService.UpdateTransaction(transaction);
                transactionLinkService.UpdateTransactionLink(link);
                if (transaction.Status == 3)
                {
                    var linkas = Url.Action("TransactionView", "MyTransaction", new { code = link.LinkCode }, Request.Scheme, Request.Host.ToString());
                    string coded = link.LinkCode.ToString();


                    var title = "Pay Guardex: Product has been shipped.";

                    var mailbody = "<html>" +
                    "<head>" +
                    "<link rel=\"stylesheet\" href=\"https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css\" integrity=\"sha384-9aIt2nRpC12Uk9gS9baDl411NQApFmC26EwAOH8WgZl5MYYxFfc+NcPb1dKGj7Sk\" crossorigin=\"anonymous\">" +
                    "</head>" +
                    "<body>" +
                    "<div class=\"container\">" +
                    "<div class=\"row\">" +
                    "<div class=\"col-md-12\" style=\"padding:30px;background-color:#d8e9ff\">" +
                    "<h3 style=\"color:forestgreen\">Product has been Shipped</h3>" +
                    "<div>" +
                        "<p>Seller shipped the product. When buyer recieved the product will update the status using the link </p>" +
                    "</div>" +
                    "<div>" +
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
                   
                    emailSender.Post(
                                   subject: title,
                                   body: mailbody,
                                   recipients: transaction.BuyerEmail,
                                   sender: configuration["AdminContact"]);
                    emailSender.Post(
                                   subject: title,
                                   body: mailbody,
                                   recipients: transaction.SellerEmail,
                                   sender: configuration["AdminContact"]);
                }
                return Json(new { success = true });
            }
            catch(Exception ex)
            {
                return Json(new { success = false });

            }
        }
        [HttpGet]
        public IActionResult FindTransaction()
        {
            FindModel model = new FindModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult CalculateFee(IFormCollection form)
        {
            CalculateHVM calculateHVM = JsonConvert.DeserializeObject<CalculateHVM>(form["modelInfo"]);
            try
            {
                CalculateGetVM calculate = new CalculateGetVM();
                int feeID = GetFeeIDByAmount(calculateHVM.Amount);
                var ef = calculate.escrowfeePercentage = feeService.GetFee(feeID).Percentage;
                var efp = (calculateHVM.Amount * ef) / 100;
                return Json(new { success = true, efp = efp });

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
                if (am >= item.Amount1 && am <= item.Amount2)
                {
                    count = count + 1;
                    var feelist = feeService.GetFees().Where(s => s.Amount1 == item.Amount1 && s.Amount2 == item.Amount2).ToList().FirstOrDefault();
                    feeeid = feelist.Id;
                }
            }
            var aa = feeeid;
            return aa;
        }
    }
}
