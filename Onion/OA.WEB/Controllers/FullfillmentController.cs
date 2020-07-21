using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OA.DATA.Entities;

namespace OA.WEB.Controllers
{
    public class FullfillmentController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;

        public FullfillmentController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (roleManager.Roles.Count() == 0)
            {
                IdentityRole model = new IdentityRole();
                IdentityRole model1 = new IdentityRole();
                model.Name = "Admin";
                model1.Name = "Client";
                await roleManager.CreateAsync(model);
                await roleManager.CreateAsync(model1);
            }
            var check = userManager.FindByEmailAsync("sysadmin@admin.com");
            if (check.Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.PhoneNumber = "12323223";
                user.AddDate = DateTime.Now;
                user.Status = 1;
                user.UserTypeID = 1;
                user.UserName = "Payguardex";
                user.Email = "sysadmin@admin.com";
                var password = "PayAdminPay";
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
            return View();
        }
    }
}
