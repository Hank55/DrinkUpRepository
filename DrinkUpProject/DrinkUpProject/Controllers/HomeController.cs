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
            
            return View(new HomeIndexVM());
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Index(SearchVM homeIndexVM)
        {
            var valueOf = Request.Form["SearchParameter"];

            HomeResultVM[] drinks;
            if (String.IsNullOrWhiteSpace(homeIndexVM.SearchItem))
                return View();

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

        [Route("CreateUser")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(HomeCreateUserVM model)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            repository.AddUser(model);

            return RedirectToAction(nameof(Index));


        }

        [Route("Test")]
        [HttpGet]
        public async Task<IActionResult> Test()
        {
            return View();
        }
    }
}
