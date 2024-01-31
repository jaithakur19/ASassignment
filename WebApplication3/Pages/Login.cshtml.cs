using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Security.Claims;
using System.Text.Json;
using WebApplication3.Model;
using WebApplication3.ViewModels;

namespace WebApplication3.Pages
{
    public class LoginModel : PageModel
    {

		[BindProperty]
		public Login LModel { get; set; }

		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly UserManager<ApplicationUser> UserManager;

		public LoginModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> UserManager)
		{
			this.signInManager = signInManager;
			this.UserManager = UserManager;
		}

		public void OnGet()
        {
        }

		public bool ValidateCaptcha()
		{
			string Response = Request.Form["g-recaptcha-response"];
			bool valid = false;

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.google.com/recaptcha/api/siteverify?secret=6LcgRGIpAAAAABjjk32_h7MnmQIS_zXB6KGw2Ev7&response=" + Response);
			try
			{
				using (WebResponse wResponse = request.GetResponse())
				{
					using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
					{
						string jsonResponse = readStream.ReadToEnd();
						var data = JsonSerializer.Deserialize<CapRes>(jsonResponse);
						valid = Convert.ToBoolean(data.success);
					}
				}
				return valid;
			}
			catch (WebException ex)
			{
				throw ex;
			}

		}


		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
				if (!ValidateCaptcha())
				{
					ModelState.AddModelError("", "Captcha is not valid");
					return Page();
				}

				var identityResult = await signInManager.PasswordSignInAsync(LModel.Email, LModel.Password,
				LModel.RememberMe, true);
				if (identityResult.Succeeded)
				{
					var user = await UserManager.FindByEmailAsync(LModel.Email);
					string guid = Guid.NewGuid().ToString();
					if (user!=null) {
						HttpContext.Session.SetString("AuthenticationToken", guid);
						user.AuthenticationToken = guid;
						await UserManager.UpdateAsync(user);

					}

					return RedirectToPage("Index");
				}
				ModelState.AddModelError("", "Username or Password incorrect");
				if (identityResult.IsLockedOut)
				{
					ModelState.AddModelError("", "account locked out");
				}

            }
			return Page();
		}
	}
}
