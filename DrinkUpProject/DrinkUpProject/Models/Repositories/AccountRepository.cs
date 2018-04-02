using DrinkUpProject.Models.Entities;
using DrinkUpProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public void addDrinkToList(Models.Entities.User user, string drinkId)
        {
            winterIsComingContext.UserDrinkList.Add(new UserDrinkList { Apiid = drinkId, KiwiUserId = user.Id, KiwiUser = winterIsComingContext.User.Find(user.Id) });
            winterIsComingContext.SaveChanges();
        }

        public void removeDrinkFromList(Models.Entities.User user, string drinkId)
        {
            var d = winterIsComingContext.UserDrinkList
                .Where(o => o.KiwiUserId == user.Id && o.Apiid == drinkId)
                .First();
            winterIsComingContext.UserDrinkList.Remove(d);
            winterIsComingContext.SaveChanges();
        }

        public void removeDrinkFromList(int userId, string drinkId)
        {
            UserDrinkList d = winterIsComingContext.UserDrinkList
                .Where(o => o.KiwiUserId == userId && o.Apiid == drinkId)
                .First();
            winterIsComingContext.UserDrinkList.Remove(d);
            winterIsComingContext.SaveChanges();
        }
    }
}
