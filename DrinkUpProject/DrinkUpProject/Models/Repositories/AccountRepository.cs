using DrinkUpProject.Models.Entities;
using DrinkUpProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DrinkUpProject.Models.Repositories
{
    public class AccountRepository
    {

        WinterIsComingContext winterIsComingContext;
        IdentityDbContext identityContext;
        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;

        public AccountRepository(
            IdentityDbContext identityContext,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            WinterIsComingContext winterIsComingContext

            )
        {
            this.identityContext = identityContext;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.winterIsComingContext = winterIsComingContext;
        }

        public async Task<bool> TryLoginAsync(AccountLoginVM viewModel)
        {
            // Create DB schema (first time)
            //var createSchemaResult = await identityContext.Database.EnsureCreatedAsync();

            // Create a hard coded user (first time)
            //var createResult = await userManager.CreateAsync(new IdentityUser("user"), "Password_123"));

            var loginResult = await signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, false, false);
            return loginResult.Succeeded;
        }


        internal async Task AddUserAsync(GuestCreateUserVM model)
        {
            var result = await userManager.CreateAsync(new IdentityUser(model.UserName), model.Password);

            var userId = identityContext.Users.Single(u => u.UserName == model.UserName).Id;


            winterIsComingContext.User.Add(new Models.Entities.User { FirstName = model.FirstName, LastName = model.LastName, FavDrink = model.FavouriteDrink, IdentityUsersId = userId });

            winterIsComingContext.SaveChanges();
        }



        //private async Task<RecentlySavedVM[]> MethodRecentlySavedAsync()
        //{
        //    var currentUser = await userManager.GetUserId(HttpContent.User);

        //    var userDrinks = currentUser
        //        .UserListDrinkId
        //        .First();

        //    List<Drink> test = new List<Drink>()
        //    {
        //       new Drink{ idDrink = userDrinks}
        //    };


        //    var user = Ge.User;

        //    var drinkById = await GetDrinksById(user);

        //    RecentlySavedVM[] recent = new RecentlySavedVM[drinkById.Count];

        //    for (int i = 0; i < 1; i++)
        //    {
        //        recent[i] = new RecentlySavedVM { DrinkName = drinkById[i].strDrink, ImgUrl = drinkById[i].strDrinkThumb };
        //    }

        //    return recent;
        //}
    }
}
