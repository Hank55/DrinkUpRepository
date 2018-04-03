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
    public class GuestController : Controller
    {
        TestRepository repository = new TestRepository();
        AccountRepository accountRepository;

        public GuestController(AccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(new GuestIndexVM());
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Index(SearchVM homeIndexVM)
        {
            var valueOf = Request.Form["SearchParameter"];

            GuestResultVM[] drinks;
            if (String.IsNullOrWhiteSpace(homeIndexVM.SearchItem))
                return View(new GuestIndexVM());

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
            if (drinks[0].DrinkName == "NoSearchResult")
            {
                return RedirectToAction(nameof(Index));
            }

            return View(drinks);

        }

        [Route("Test")]
        [HttpGet]
        public async Task<IActionResult> Test()
        {
            //accountRepository.removeDrinkFromList(9, "11102");
            return View();
        }


        [Route("AboutUs")]
        [HttpGet]
        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
