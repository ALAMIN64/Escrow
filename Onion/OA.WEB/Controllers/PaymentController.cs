using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OA.DATA.Entities;
using OA.Service;
using OA.WEB.Models.ViewModel;

namespace OA.WEB.Controllers
{
    public class PaymentController : Controller
    {
        private IPaymentService paymentService;
        private ITransactionLinkService transactionLinkService;
        private ITransactionService transactionService;
        private IWebHostEnvironment env;

        public PaymentController(IPaymentService paymentService,
            ITransactionLinkService transactionLinkService,
            ITransactionService transactionService,
            IWebHostEnvironment env)
        {
            this.paymentService = paymentService;
            this.transactionLinkService = transactionLinkService;
            this.transactionService = transactionService;
            this.env = env;
        }
        [HttpGet]
        public IActionResult AddPayment(int id)
        {
            var linkdetails = transactionLinkService.GetTransactionLink(id);
            var transaction = transactionService.GetTransaction(linkdetails.TransactionID);
            PaymentVM payment = new PaymentVM();
            payment.LinkID = linkdetails.Id;
            payment.Amount = transaction.Totalfee;
            return View(payment);
        }
        [HttpPost]
        public IActionResult PayInsert(IFormCollection form)
        {
            PaymentVM payment = JsonConvert.DeserializeObject<PaymentVM>(form["modelInfo"]);
            try
            {
                Payment dbpay = new Payment();
                dbpay.Amount = payment.Amount;
                dbpay.AccountNumber = payment.AccountNumber;
                dbpay.BankName = payment.BankName;
                dbpay.Description = payment.Description;
                dbpay.LinkID = payment.LinkID;
                dbpay.AddDate = DateTime.Now;
                dbpay.Status = 1;
                int i = 0;
                var uploadFiles = Request.Form.Files;
                foreach (var file in uploadFiles)
                {
                    Guid nameguid = Guid.NewGuid();
                    string webrootpath = env.WebRootPath;
                    string filename = nameguid.ToString();
                    string extension = Path.GetExtension(file.FileName);
                    string foldername = "TransactionImage";
                    filename = filename + extension;
                    string path = Path.Combine(webrootpath, foldername, filename);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    string pathName = Path.Combine(foldername, filename);
                    if (i == 0)
                    {
                        dbpay.DocumentsUrl = "~/TransactionImage/" + filename;
                    }
                    if (i == 1)
                    {
                        dbpay.DocumentsUrl2 = "~/TransactionImage/" + filename;
                    }
                    if (i == 2)
                    {
                        dbpay.DocumentsUrl3 = "~/TransactionImage/" + filename;
                    }

                    i++;
                }
                paymentService.InsertPayment(dbpay);
                return Json(new { success = true });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
