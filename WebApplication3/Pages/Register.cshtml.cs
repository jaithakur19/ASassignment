using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web;
using WebApplication3.Model;
using WebApplication3.ViewModels;

namespace WebApplication3.Pages
{
    public class RegisterModel : PageModel
    {

        private UserManager<Model.ApplicationUser> userManager { get; }
        private SignInManager<Model.ApplicationUser> signInManager { get; }

        [BindProperty]
        public Register RModel { get; set; }

        public RegisterModel(UserManager<Model.ApplicationUser> userManager,
        SignInManager<Model.ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }



        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

				var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
				var protector = dataProtectionProvider.CreateProtector("MySecretKey");

                var user = new ApplicationUser()
                {
                    UserName = HttpUtility.HtmlEncode(RModel.Email),
			        FullName = HttpUtility.HtmlEncode(RModel.FullName),
                    Credit = protector.Protect(RModel.Credit),
                    SelectedGender = RModel.SelectedGender,
                    Phone = HttpUtility.HtmlEncode(RModel.Phone),
                    Address = HttpUtility.HtmlEncode(RModel.Address),
                    Email = HttpUtility.HtmlEncode(RModel.Email),
                    
                    AboutMe = HttpUtility.HtmlEncode(RModel.AboutMe),
                    
                };
                var result = await userManager.CreateAsync(user, RModel.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToPage("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Page();
        }







    }
}
