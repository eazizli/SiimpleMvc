using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SiimpleMvc.AccountViewModel;
using SiimpleMvc.Models;
using System.Linq.Expressions;

namespace SiimpleMvc.Controllers
{
    public class AcoountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AcoountController(UserManager<AppUser> userManager, SignInManager<AppUser> signinManager)
        {
            _userManager = userManager;
            _signInManager = signinManager;
        }

        
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task< IActionResult> Register(RegisterVM registervm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = new AppUser()
            {
                Name = registervm.Name,
                Surname = registervm.Surname,
                Email = registervm.Email,
                UserName = registervm.Username,

            };
            IdentityResult result = await _userManager.CreateAsync(user,registervm.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            return RedirectToAction("Index", "Home");
            
        }
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = await _userManager.FindByEmailAsync(login.Email);
            if(user == null)
            {
                ModelState.AddModelError("" ,"UserNAme and paswor incorrect");
                return View();
            }
            Microsoft.AspNetCore.Identity.SignInResult sign = await _signInManager.PasswordSignInAsync(user, login.Password, true, false);
            if (!sign.Succeeded)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
