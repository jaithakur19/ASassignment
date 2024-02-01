using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.Model;

namespace WebApplication3.Pages
{
    public class LogoutModel : PageModel
    {
		private readonly AuthDbContext authDbContext;
		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly UserManager<ApplicationUser> UserManager;
		public LogoutModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> UserManager, AuthDbContext authDbContext)
		{
			this.signInManager = signInManager;
			this.UserManager = UserManager;
			this.authDbContext = authDbContext;
		}
		public void OnGet() { 
		}
		public async Task<IActionResult> OnPostLogoutAsync()
		{
			var user = await UserManager.GetUserAsync(User);
            if (user != null)
            {
                var log = new AuditLog
                {
                    UserId = user.Id,
                    Action = "Logged Out",
                    Time = DateTime.Now
                };

                authDbContext.AuditLogTable.Add(log);
                authDbContext.SaveChanges();
                
                

            }
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
