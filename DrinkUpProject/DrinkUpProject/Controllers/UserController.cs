using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrinkUpProject.Models.Repositories;
using DrinkUpProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var randomDrink = await repository.GetRandomFactAboutDrink();

            return View(randomDrink);
        }

        [Route("MyPage")]
        public IActionResult MyPage()
        {
            return View();
        }

        //[HttpGet]
        //[Route("Recipe")]
        //[HttpGet]
        //public async Task<IActionResult> Recipe()
        //{
        //    return View(new UserRecipeVM());
        //}

        [Route("Recipe")]
        [HttpGet]
        public async Task<IActionResult> Recipe(string drinkId)
        {
            return View(await repository.GetRecipe(drinkId));
        }

        //[HttpPost]
        //[Route("Recipe")]
        //public async Task<IActionResult> Recipe(UserRecipeVM recipe)
        //{
        //    accountRepository.addDrinkToList(recipe.LoggedInAs.UserName, recipe.RecipeDrink.idDrink);
        //    return View();
        //}
    }
}
