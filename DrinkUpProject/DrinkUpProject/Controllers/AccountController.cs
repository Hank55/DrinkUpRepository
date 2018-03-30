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
        [Route("login")]
        public IActionResult Login(string returnUrl)
        {
            var model = new AccountLoginVM { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        [Route("logIn")]
        public async Task<IActionResult> Login(AccountLoginVM viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            // Check if credentials is valid (and set auth cookie)
            if (!await repository.TryLoginAsync(viewModel))
            {
                // Show login error
                ModelState.AddModelError(nameof(AccountLoginVM.Username), "Invalid credentials");
                return View(viewModel);
            }

            // Redirect user
            if (string.IsNullOrWhiteSpace(viewModel.ReturnUrl))
                return RedirectToAction(nameof(UserHomeController.Home));
            else
                return Redirect(viewModel.ReturnUrl);
        }

        [Route("CreateUser")]
        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {

            return View();
        }

        [Route("CreateUser")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(HomeCreateUserVM model)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            await repository.AddUserAsync(model);

            return RedirectToAction(nameof(UserHomeController.Home));


        }
    }
}
