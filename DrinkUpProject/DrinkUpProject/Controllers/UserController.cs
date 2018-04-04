using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DrinkUpProject.Models.Repositories;
using DrinkUpProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DrinkUpProject.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        TestRepository repository = new TestRepository();
        AccountRepository accountRepository;

        public UserController(AccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        [Route("Home")]
        public async Task<IActionResult> Home()
        {
            var user = HttpContext.User;
            var randomDrink = await accountRepository.GetRandomFactAboutDrink(user);

            return View(randomDrink);
        }


        [Route("MyPage")]
        public async Task<IActionResult> MyPageAsync()
        {
            var model = await accountRepository.FindDrinkListByUserIdAsync(User);
            return View(model);
        }

        [HttpGet]
        [Route("Recipe/{id}")]
        public async Task<IActionResult> Recipe(string id)
        {
            return View(await accountRepository.GetRecipe(id));
        }

        [HttpPost]
        [Route("Recipe/{id}")]
        public async Task<IActionResult> SaveRecipe(string id)
        {
            var userDetails = accountRepository.GetLoggedInUser(User.Identity);
            accountRepository.addDrinkToList(userDetails.UserName, id);
            return View(nameof(Recipe), await accountRepository.GetRecipe(id));
        }

       

    }
}
