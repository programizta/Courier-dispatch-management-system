using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dispatch_system.Data;
using Dispatch_system.Models;
using Dispatch_system.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dispatch_system.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ApplicationDbContext context;

        public AccountController(UserManager<IdentityUser> userManager,
                                 SignInManager<IdentityUser> signInManager,
                                 ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };

                var result = await userManager.CreateAsync(user, model.Password);
                await userManager.AddToRoleAsync(user, "Klient");
                string userId = userManager
                                .Users.FirstOrDefault(u => u.Email == model.Email)
                                .Id;

                Person newPerson = new Person
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    UserId = userId
                };

                context.Add(newPerson);
                context.SaveChanges();

                int personId = context.People.FirstOrDefault(x => x.UserId == userId).PersonId;

                UserAddress newUserAddress = new UserAddress
                {
                    PersonId = personId,
                    StreetName = model.StreetName,
                    BlockNumber = model.BlockNumber,
                    FlatNumber = model.FlatNumber,
                    PostalCode = model.PostalCode,
                    City = model.City
                };

                context.Add(newUserAddress);
                context.SaveChanges();

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }

                ModelState.AddModelError(string.Empty, "Podano błędny adres e-mail lub hasło");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public IActionResult PersonalData()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PersonalData(int personId)
        {
            throw new NotImplementedException();
        }
    }
}
