using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OA.DATA.Entities;
using OA.Service;
using OA.WEB.Models.ViewModel;

namespace OA.WEB.Controllers
{
    [Authorize]
    public class UserManageController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private ITransactionService transactionService;

        public UserManageController(UserManager<ApplicationUser> userManager, ITransactionService transactionService)
        {
            this.userManager = userManager;
            this.transactionService = transactionService;
        }
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult UserList()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetAllUsers()
        {
            JsonResult result;
            try
            {
                string search = HttpContext.Request.Form["search[value]"][0];
                string draw = HttpContext.Request.Form["draw"][0];
                string order = HttpContext.Request.Form["order[0][column]"][0];
                string orderDir = HttpContext.Request.Form["order[0][dir]"][0];
                int startRec = Convert.ToInt32(HttpContext.Request.Form["start"][0]);
                int pageSize = Convert.ToInt32(HttpContext.Request.Form["length"][0]);
                int totalRecords = 0;
                var firstPartOfQuery = userManager.Users.Where(s => s.UserTypeID == 2).AsQueryable();
                int ifSearch = 0;
                List<UserVM> data = new List<UserVM>();
                var secondPartOfQuery = firstPartOfQuery.AsEnumerable();

                if (secondPartOfQuery.Count() > 0)
                {
                    totalRecords = secondPartOfQuery.AsEnumerable().Count();
                    data = secondPartOfQuery.AsEnumerable().Skip(startRec).Take(pageSize).Select(

                            s => new UserVM
                            {
                                id = s.Id,
                                FirstName = s.FirstName == null ? "Not set" : s.FirstName,
                                LastName = s.LastName == null ? "Not set" : s.LastName,
                                Email = s.Email,
                                Phone = s.PhoneNumber,
                                Transaction = transactionService.GetTransactions().Where(x => x.SellerID == s.Id).Count()
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

        private List<UserVM> SortByColumnWithOrder(string order, string orderDir, List<UserVM> data)
        {
            List<UserVM> lst = new List<UserVM>();
            try
            {
                switch (order)
                {

                    case "0":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.id).ToList() : data.OrderBy(p => p.id).ToList();
                        break;
                    case "1":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.FirstName).ToList() : data.OrderBy(p => p.FirstName).ToList();
                        break;
                    case "2":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.LastName).ToList() : data.OrderBy(p => p.LastName).ToList();
                        break;
                    case "3":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Email).ToList() : data.OrderBy(p => p.Email).ToList();
                        break;
                    case "4":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Phone).ToList() : data.OrderBy(p => p.Phone).ToList();
                        break;
                    case "5":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Transaction).ToList() : data.OrderBy(p => p.Transaction).ToList();
                        break;

                    default:
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.id).ToList() : data.OrderBy(p => p.id).ToList();
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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteSeller(string id)
        {
            try
            {
                var user = await userManager.FindByIdAsync(id);
                await userManager.DeleteAsync(user);
                var JSON = Json(new { success = true });
                return JSON;
            }
            catch (Exception ex)
            {
                var JSON = Json(new { success = false });

                return JSON;
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EmployeeList()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetAllEmployees()
        {
            JsonResult result;
            try
            {
                string search = HttpContext.Request.Form["search[value]"][0];
                string draw = HttpContext.Request.Form["draw"][0];
                string order = HttpContext.Request.Form["order[0][column]"][0];
                string orderDir = HttpContext.Request.Form["order[0][dir]"][0];
                int startRec = Convert.ToInt32(HttpContext.Request.Form["start"][0]);
                int pageSize = Convert.ToInt32(HttpContext.Request.Form["length"][0]);
                int totalRecords = 0;
                var firstPartOfQuery = userManager.Users.Where(s => s.UserTypeID == 1).AsQueryable();
                int ifSearch = 0;
                List<EmployeeVM> data = new List<EmployeeVM>();
                var secondPartOfQuery = firstPartOfQuery.AsEnumerable();

                if (secondPartOfQuery.Count() > 0)
                {
                    totalRecords = secondPartOfQuery.AsEnumerable().Count();
                    data = secondPartOfQuery.AsEnumerable().Skip(startRec).Take(pageSize).Select(

                            s => new EmployeeVM
                            {
                                id = s.Id,
                                FirstName = s.FirstName == null ? "Not set" : s.FirstName,
                                LastName = s.LastName == null ? "Not set" : s.LastName,
                                Email = s.Email,
                                Phone = s.PhoneNumber,
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

        private List<EmployeeVM> SortByColumnWithOrderforEmployee(string order, string orderDir, List<EmployeeVM> data)
        {
            List<EmployeeVM> lst = new List<EmployeeVM>();
            try
            {
                switch (order)
                {

                    case "0":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.id).ToList() : data.OrderBy(p => p.id).ToList();
                        break;
                    case "1":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.FirstName).ToList() : data.OrderBy(p => p.FirstName).ToList();
                        break;
                    case "2":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.LastName).ToList() : data.OrderBy(p => p.LastName).ToList();
                        break;
                    case "3":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Email).ToList() : data.OrderBy(p => p.Email).ToList();
                        break;
                    case "4":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Phone).ToList() : data.OrderBy(p => p.Phone).ToList();
                        break;

                    default:
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.id).ToList() : data.OrderBy(p => p.id).ToList();
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
        public async Task<ActionResult> InsertEmployee(IFormCollection form)
        {
            EmployeeVM emp = JsonConvert.DeserializeObject<EmployeeVM>(form["EmployeeInfo"]);
            var isValid = IsValidEmail(emp.Email);
            if (isValid == false)
            {
                return Json(new { success = false, message = "Email is invalid" });
            }
            var usercheck = await userManager.FindByEmailAsync(emp.Email ?? "");
            if (usercheck != null)
            {
                return Json(new { success = false, message = "Email is already exists." });
            }
            try
            {
                ApplicationUser user = new ApplicationUser();
                user.FirstName = emp.FirstName;
                user.LastName = emp.LastName;
                user.Email = emp.Email;
                user.PhoneNumber = emp.Phone;
                user.AddDate = DateTime.Now;
                user.Status = 1;
                user.UserTypeID = 1;
                user.UserName = emp.Email;
                var result = await userManager.CreateAsync(user, emp.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = result.Errors.FirstOrDefault().Description });
                }
            }
            catch (Exception ex)
            {
                var des = ex.Message.ToString();
                return Json(new { success = false, message = des });
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteEmployee(string id)
        {
            try
            {
                var user = await userManager.FindByIdAsync(id);
                await userManager.DeleteAsync(user);
                var JSON = Json(new { success = true });
                return JSON;
            }
            catch (Exception ex)
            {
                var JSON = Json(new { success = false });

                return JSON;
            }

        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}
