using DrinkUpProject.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkUpProject.Views.Shared.Components.NavBarLoggedIn
{
    public class NavBarLoggedInViewComponent
    {
        public class LoggedInAsUserViewComponent : ViewComponent
        {
            private AccountRepository repository;

            public LoggedInAsUserViewComponent(AccountRepository repository)
            {
                this.repository = repository;
            }


            public IViewComponentResult InvokeAsync()
            {
                return View(repository.GetLoggedInUser(User.Identity));
            }
        }
    }
}
