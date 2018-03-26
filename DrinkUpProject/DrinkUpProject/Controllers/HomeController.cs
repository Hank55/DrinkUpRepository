﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrinkUpProject.Models;
using DrinkUpProject.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DrinkUpProject.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        TestRepository repository = new TestRepository();

        public async Task<IActionResult> Index()
        {
            return View(await repository.GetRandomDrink());
        }
    }
}
