using DrinkUpProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DrinkUpProject.Views.Shared.Components.HeaderLogo

{
    public class HeaderLogoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(new IsLoggedInVM {IsloggedIn = User.Identity.IsAuthenticated});
        }
    }
}
