using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.Model;

namespace WebApplication3.Pages
{
    public class LogoutModel : PageModel
    {
		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly UserManager<ApplicationUser> UserManager;
		public LogoutModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> UserManager)
		{
			this.signInManager = signInManager;
			this.UserManager = UserManager;
		}
		public void OnGet() { 
		}
		public async Task<IActionResult> OnPostLogoutAsync()
		{
			var user = await UserManager.GetUserAsync(User);
			user.AuthenticationToken = null;
			await UserManager.UpdateAsync(user);


			

			await signInManager.SignOutAsync();
			HttpContext.Session.Clear();
			return RedirectToPage("Login");
		}
		public async Task<IActionResult> OnPostDontLogoutAsync()
		{
			return RedirectToPage("Index");
		}

	}
}
