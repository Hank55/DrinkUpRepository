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
    [Route("")]
    public class HomeController : Controller
    {
        TestRepository repository = new TestRepository();

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            
            return View();
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Index(string searchResult)
        {
            var valueOf = Request.Form["SearchParameter"];

            if (valueOf == "Drink")
            {
                
            var drink  = await repository.SearchResultDrinkName(searchResult);
            }
            else
            { 
                var ingredient = await repository.SearchResultIngredient(searchResult);
            }


            return RedirectToAction(nameof(SearchResult));
        }

        [Route("/RandomDrink")]
        public async Task<IActionResult> RandomDrink()
        {
            return View(await repository.GetRandomDrink());
        }


        [Route("/Results")]
        public async Task<IActionResult> SearchResult(string drinkName)
        {
            return View(await repository.SearchResultDrinkName(drinkName));

        }

    }
}
