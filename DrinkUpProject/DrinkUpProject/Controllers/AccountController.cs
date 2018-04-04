using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrinkUpProject.Models;
using DrinkUpProject.Models.Repositories;
using DrinkUpProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DrinkUpProject.Controllers
{
    public class AccountController : Controller
    {
        AccountRepository repository;

        public AccountController(AccountRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [Route("LogIn")]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [Route("LogIn")]
        public async Task<IActionResult> LogIn(GuestIndexLogInVM viewModel)
        {
            if (!ModelState.IsValid)
                return View("/Views/Guest/Index.cshtml", new GuestIndexVM { LogInForm = viewModel });



            // Check if credentials is valid (and set auth cookie)
            if (!await repository.TryLoginAsync(viewModel))
            {
                // Show login error
                ModelState.AddModelError(nameof(GuestIndexVM.LogInForm), "Invalid credentials");
                return RedirectToAction(nameof(GuestController.Index), "Guest");
            }
            else
                return RedirectToAction(nameof(UserController.Home), "User");
        }

        [Route("CreateUser")]
        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {
            return View("/Views/Guest/CreateUser.cshtml", new GuestCreateUserVM());
        }

        [Route("CreateUser")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(GuestCreateUserVM model)
        {

            if (!ModelState.IsValid)
            {
                return View("/Views/Guest/CreateUser.cshtml", model);
            }

            await repository.AddUserAsync(model);

            await repository.TryLoginAsync(new GuestIndexLogInVM { UserName = model.UserName, Password = model.Password });



            return RedirectToAction(nameof(UserController.Home), "User");
        }


        [HttpPost]
        [Route("LogOut")]
        public IActionResult Logout()
        {
            repository.Logout();

            return RedirectToAction(nameof(GuestController.Index), "Guest");
        }
    }
}
