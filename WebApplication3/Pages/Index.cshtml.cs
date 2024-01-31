﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.Model;

namespace Application_Security_Assignment.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private UserManager<ApplicationUser> userManager { get; }
        private SignInManager<ApplicationUser> signInManager { get; }
        public IndexModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public async Task OnGet()
        {
            var user = await userManager.GetUserAsync(User);

            if (user != null){
                if (user.AuthenticationToken == HttpContext.Session.GetString("AuthenticationToken"))
                {
					HttpContext.Session.SetString("Email", user.Email);
					HttpContext.Session.SetString("FullName", user.FullName);
					HttpContext.Session.SetString("SelectedGender", user.SelectedGender);
					HttpContext.Session.SetString("Phone", user.Phone);
					HttpContext.Session.SetString("Address", user.Address);

					var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
					var protector = dataProtectionProvider.CreateProtector("MySecretKey");

					HttpContext.Session.SetString("Credit", protector.Unprotect(user.Credit));
				}
                else
                {
                    await signInManager.SignOutAsync();
                    HttpContext.Session.Clear();
                    Response.Redirect("Login");
                }
                    


               }

        }
    }
}