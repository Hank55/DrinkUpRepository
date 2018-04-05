using DrinkUpProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DrinkUpProject.Views.Shared.Components.LogoWhenLoggedIn
{
    public class LogoWhenLoggedInViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(new LogoVM { IsLogoWhenLoggedIn = User.Identity.IsAuthenticated });
        }
    }
}
 
