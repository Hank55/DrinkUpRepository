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
        public async Task<IActionResult> Index(HomeIndexVM homeIndexVM)
        {
            var valueOf = Request.Form["SearchParameter"];

            HomeResultVM[] drinks;

            if (valueOf == "Drink")
            {
                drinks = await repository.SearchResultDrinkName(homeIndexVM.SearchItem);
            }
            else
                drinks = await repository.SearchResultIngredient(homeIndexVM.SearchItem);
            
            


            return RedirectToAction(nameof(SearchResult));
        }

        [Route("RandomDrink")]
        public async Task<IActionResult> RandomDrink()
        {
            return View(await repository.GetRandomDrink());
        }


        [Route("SearchResult")]
        public async Task<IActionResult> SearchResult()
        {
            var drinks = repository.GetLastSearchResult();


            return View(drinks);

        }

        [Route("CreateUser")]
        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {

            return View();
        }
    }
}
