using ClothesStore.Models;
using ClothesStore.Repositories;
using ClothesStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClothesStore.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthRepository _authRepository;
        private readonly UserManager<ApplicationUserz> _userManager;
        private readonly SignInManager<ApplicationUserz> _signInManager;

        public AuthController(
            IAuthRepository authRepository, 
            UserManager<ApplicationUserz> userManager,
            SignInManager<ApplicationUserz> signInManager)
        {
            _authRepository = authRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginViewModel model = new LoginViewModel() { Email = "", Password = "" };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // show error message
                return View(model);
            }
            // check login
            ApplicationUserz user = await _authRepository.LoginAsync(model.Email, model.Password);
            await _signInManager.SignInAsync(user, false);
            return  RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            RegisterViewModel model = new() { Email = "", Password = "" };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // show error message
                return View(model);
            }
            ApplicationUserz user = new() { UserName = model.Email, Email = model.Email };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                ViewBag.RegisterErrors = result.Errors.Select(e => e.Description).ToList();
                // show error message
                return View(model);
            }

            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }
    }
}
