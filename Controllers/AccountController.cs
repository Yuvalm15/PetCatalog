using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShopProject.DAL;
using PetShopProject.Models;

namespace PetShopProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _ = CreateRolesAndAdmin().Result;

        }
        [HttpGet]
        public IActionResult LogIn()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username!, model.Password!, false, false);
                if (result.Succeeded)
                {
                    var user = _userManager.FindByNameAsync(model.Username!).Result;
                    User.SetIconPath(user!.IconImageUrl);
                    User.SetDateTime(user!.CreatedAt);
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.ErrorMessage = "The input for your username or your password is incorrect, please try again!";

            }

            return View();
        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ViewBag.Picture == null)
            {
                ViewBag.Picture = "/Assets/UserPictures/Default.jpg";
            }
            ViewBag.ErrorMessage = "";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterConfirmed(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                UserModel user = new UserModel
                {
                    UserName = model.Username,
                    IconImageUrl = model.IconImageUrl,
                };
                if (await _userManager.FindByNameAsync(user.UserName!) != null)
                {
                    ViewBag.ErrorMessage = "UserName Allready Exist";
                    ViewBag.Name = model.Username;
                    ViewBag.Pass = model.Password;
                    ViewBag.Picture = model.IconImageUrl;
                    return View("Register");
                }

                try
                {
                    var result = await _userManager.CreateAsync(user, model.Password!);
                    await _userManager.AddToRoleAsync(user, "Users");

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                }
                catch(Exception)
                {
                    ViewBag.ErrorMessage = "Failed to create User, Please make sure your password is atleast 4 character longs contains atleast one number, one uppercase and one lowercase";
                }
                ViewBag.Picture = model.IconImageUrl;
                return View("Register");
            }
            return View();
        }
        public async Task<IActionResult> CreateRolesAndAdmin()
        {
            IdentityRole role = new IdentityRole();
            IdentityRole role2 = new IdentityRole();
            role.Name = "Administrators";
            await _roleManager.CreateAsync(role);
            role2.Name = "Users";
            await _roleManager.CreateAsync(role2);

            UserModel user = new UserModel
            {
                UserName = "brave",
                IconImageUrl = "/Assets/UserPictures/Default.jpg"
            };

            if (await _userManager.FindByNameAsync(user.UserName) != null)
            {
                return RedirectToAction("LogIn");
            }



            await _userManager.CreateAsync(user, "123Ab");
            await _userManager.AddToRoleAsync(user, "Administrators");
            return Content("succes");
        }
    }
}
