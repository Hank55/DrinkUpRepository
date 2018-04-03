using DrinkUpProject.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkUpProject.Views.Shared.Components.LoggedInAsUser
{
    public class LoggedInAsUserViewComponent : ViewComponent
    {
        private AccountRepository repository;

        public LoggedInAsUserViewComponent(AccountRepository repository)
        {
            this.repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            return View(repository.GetLoggedInUser(User.Identity));
        }
    }


}
