using JofApp.Helpers;
using JofApp.Models;
using JofApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JofApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm registerVm)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            AppUser appUser = new AppUser()
            {
                Name = registerVm.Name,
                Surname = registerVm.Surname,
                UserName = registerVm.UserName,
                Email = registerVm.Email
            };


        

            var result=await _userManager.CreateAsync(appUser, registerVm.Password);
            if (!result.Succeeded)
            {
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            await _userManager.AddToRoleAsync(appUser, UserRole.User.ToString());

            return RedirectToAction(nameof(Login));
        }


        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginVm loginVm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = await _userManager.FindByEmailAsync(loginVm.UsernameOrEmail);
            if(user == null)
            {
                user = await _userManager.FindByNameAsync(loginVm.UsernameOrEmail);
                if (user == null)
                {
                    throw new Exception("username veya password sehvdi");
                }
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginVm.Password,false);
            if (!result.Succeeded)
            {
                throw new Exception("username ve ya password sehvdi");
            }
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> LogOut()
        {
            await  _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }



        public async Task CreateRole()
        {

            foreach (var item in Enum.GetNames(typeof(UserRole)))
            {
                if(!await _roleManager.RoleExistsAsync(item.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole()
                    {
                        Name = item.ToString()
                    });
                }
            }
        }
    }
}
