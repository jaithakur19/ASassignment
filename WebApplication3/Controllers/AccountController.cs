using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Model;
using WebApplication3.ViewModels;

namespace WebApplication3.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                _logger.LogInformation($"User {model.Email} logged in at {DateTime.UtcNow}");
                return RedirectToAction("Index", "Home");
            }

            if (result.IsLockedOut)
            {
                // Handle account lockout, e.g., show a message to the user
                ModelState.AddModelError(string.Empty, "Account locked out. Please try again later.");
                return View(model);
            }

            _logger.LogWarning($"Failed login attempt for user {model.Email} at {DateTime.UtcNow}");

            // Handle other failed login scenarios
            return View(model);
        }

    }
}
