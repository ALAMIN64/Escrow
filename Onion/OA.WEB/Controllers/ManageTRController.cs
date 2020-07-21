using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OA.Service;
using OA.WEB.Models;
using OA.WEB.Models.ViewModel;

namespace OA.WEB.Controllers
{
    [Authorize]
    public class ManageTRController : Controller
    {
        private ITransactionService transactionService;
        private ITransactionLinkService transactionLinkService;
        private IPaymentService paymentService;

        public ManageTRController(ITransactionService transactionService, ITransactionLinkService transactionLinkService, IPaymentService paymentService)
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
        public ActionResult GetAllTransaction()
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
                var firstPartOfQuery = transactionService.GetTransactions().AsQueryable();
                int ifSearch = 0;
                var secondPartOfQuery = firstPartOfQuery;
                List<TransactionVM> data = new List<TransactionVM>();
                if (statusddl != 0)
                {
                    secondPartOfQuery = firstPartOfQuery.Where(s => s.Status == statusddl);
                }

                if (secondPartOfQuery.Count() > 0)
                {
                    totalRecords = secondPartOfQuery.AsEnumerable().Count();
                    data = secondPartOfQuery.AsEnumerable().Skip(startRec).Take(pageSize).Select(

                            s => new TransactionVM
                            {
                                ID = s.Id,
                                Email = s.BuyerEmail,
                                SellerEmail = s.SellerEmail,
                                Amount = s.Amount,
                                ItemName = s.ItemName,
                            })
                        .ToList();
                }

                data = this.SortByColumnWithOrderforEmployee(order, orderDir, data);
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

        private List<TransactionVM> SortByColumnWithOrderforEmployee(string order, string orderDir, List<TransactionVM> data)
        {
            List<TransactionVM> lst = new List<TransactionVM>();
            try
            {
                switch (order)
                {

                    case "0":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.ID).ToList() : data.OrderBy(p => p.ID).ToList();
                        break;
                    case "1":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Email).ToList() : data.OrderBy(p => p.Email).ToList();
                        break;
                    case "2":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.SellerEmail).ToList() : data.OrderBy(p => p.SellerEmail).ToList();
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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Expired()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetAllExpired()
        {
            JsonResult result;
            try
            {
                DateTime dt = DateTime.Now.AddDays(-30);
                string search = HttpContext.Request.Form["search[value]"][0];
                string draw = HttpContext.Request.Form["draw"][0];
                string order = HttpContext.Request.Form["order[0][column]"][0];
                string orderDir = HttpContext.Request.Form["order[0][dir]"][0];
                int startRec = Convert.ToInt32(HttpContext.Request.Form["start"][0]);
                int pageSize = Convert.ToInt32(HttpContext.Request.Form["length"][0]);
                int totalRecords = 0;
                var firstPartOfQuery = transactionService.GetTransactions().Where(a => a.AddDate <= dt).AsQueryable();
                int ifSearch = 0;
                var secondPartOfQuery = firstPartOfQuery;
                List<TransactionVM> data = new List<TransactionVM>();

                if (secondPartOfQuery.Count() > 0)
                {
                    totalRecords = secondPartOfQuery.AsEnumerable().Count();
                    data = secondPartOfQuery.AsEnumerable().Skip(startRec).Take(pageSize).Select(

                            s => new TransactionVM
                            {
                                ID = s.Id,
                                Email = s.BuyerEmail,
                                SellerEmail = s.SellerEmail,
                                Amount = s.Amount,
                                ItemName = s.ItemName,
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

        private List<TransactionVM> SortByColumnWithOrder(string order, string orderDir, List<TransactionVM> data)
        {
            List<TransactionVM> lst = new List<TransactionVM>();
            try
            {
                switch (order)
                {

                    case "0":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.ID).ToList() : data.OrderBy(p => p.ID).ToList();
                        break;
                    case "1":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Email).ToList() : data.OrderBy(p => p.Email).ToList();
                        break;
                    case "2":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.SellerEmail).ToList() : data.OrderBy(p => p.SellerEmail).ToList();
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

        [HttpGet]
        public IActionResult ViewDetails(int id)
        {
            StatusNames statusNames = new StatusNames();
            ViewBag.StatusEs = new SelectList(statusNames.GetStatuses(), "Status", "Name");
            var tr = transactionService.GetTransaction(id);
            var link = transactionLinkService.GetTransactionLinks().Where(s => s.TransactionID == tr.Id).FirstOrDefault();
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
        [HttpGet]
        public IActionResult ExpiredDetails(int id)
        {
            StatusNames statusNames = new StatusNames();
            ViewBag.StatusEs = new SelectList(statusNames.GetStatuses(), "Status", "Name");
            var tr = transactionService.GetTransaction(id);
            var link = transactionLinkService.GetTransactionLinks().Where(s => s.TransactionID == tr.Id).FirstOrDefault();
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
            details.Status = "Expired";

            return View(details);
        }
        [HttpPost]
        public IActionResult Delete(TransactionDetails dee)
        {
            var tr = transactionService.GetTransaction(dee.TransactionID);
            var link = transactionLinkService.GetTransactionLinks().Where(s => s.TransactionID == tr.Id).FirstOrDefault();
            var payment = paymentService.GetPayments().Where(s => s.LinkID == link.Id).FirstOrDefault();
            transactionLinkService.DeleteTransactionLink(link.Id);
            transactionService.DeleteTransaction(tr.Id);
            if (payment != null)
            {
                paymentService.DeletePayment(payment.Id);

            }
            return View(nameof(Expired));
        }
    }
}
