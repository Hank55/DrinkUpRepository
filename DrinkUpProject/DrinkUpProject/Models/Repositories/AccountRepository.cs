﻿using DrinkUpProject.Models.Entities;
using DrinkUpProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace DrinkUpProject.Models.Repositories
{
    public class AccountRepository
    {

        TestRepository testRepository = new TestRepository();

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

        public async Task<bool> TryLoginAsync(GuestIndexLogInVM viewModel)
        {
            // Create DB schema (first time)
            //var createSchemaResult = await identityContext.Database.EnsureCreatedAsync();

            // Create a hard coded user (first time)
            //var createResult = await userManager.CreateAsync(new IdentityUser("user"), "Password_123"));

            var loginResult = await signInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, false, false);
            return loginResult.Succeeded;
        }


        internal async Task AddUserAsync(GuestCreateUserVM model)
        {
            var result = await userManager.CreateAsync(new IdentityUser(model.UserName), model.Password);

            var userId = identityContext.Users.Single(u => u.UserName == model.UserName).Id;


            winterIsComingContext.User.Add(new Models.Entities.User { FirstName = model.FirstName, LastName = model.LastName, FavDrink = model.FavouriteDrink, IdentityUsersId = userId });

            winterIsComingContext.SaveChanges();
        }

        public void addDrinkToList(string userName, string drinkId)
        {
            User user = FindUserByUserName(userName);

            winterIsComingContext.UserDrinkList.Add(new UserDrinkList { Apiid = drinkId, KiwiUserId = user.Id, KiwiUser = winterIsComingContext.User.Find(user.Id) });
            winterIsComingContext.SaveChanges();
        }

        public User FindUserByUserName(string userName)
        {
            var identityUser = identityContext.Users
                .Where(o => o.UserName == userName)
                .Single();
            var user = winterIsComingContext
                .User
                .Where(o => o.IdentityUsersId == identityUser.Id)
                .Single();

            return user;
        }

        public void addDrinkToList(User user, string drinkId)
        {
            winterIsComingContext.UserDrinkList.Add(new UserDrinkList { Apiid = drinkId, KiwiUserId = user.Id, KiwiUser = winterIsComingContext.User.Find(user.Id) });
            winterIsComingContext.SaveChanges();
        }

        public void removeDrinkFromList(User user, string drinkId)
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

        public void Logout()
        {
            signInManager.SignOutAsync();
        }



        public async Task<UserHomeVM> GetRandomFactAboutDrink(ClaimsPrincipal user)
        {

            var listOfFact = new List<DrinkFacts>()
            {
                new DrinkFacts{Id = 1 , Fact = "The production of alcohol has been traced back at least 12,000 years."},
                new DrinkFacts{Id = 2 , Fact = "Frederick the Great, who was the king of Prussia, was so enamored by alcohol that he tried to ban coffee in an attempt to get everyone in Prussia to drink liquor instead."},
                new DrinkFacts{Id = 3 , Fact = "Sherry was apparently the alcohol of choice for many world travelers; both Magellan and Columbus had a lot of it on board during their respective voyages. Magellan liked Sherry so much, in fact, that he spent more money stockpiling the alcoholic beverage than he spent on weapons."},
                new DrinkFacts{Id = 4 , Fact = "The Pilgrims made the decision to stop at Plymouth Rock because they were running low on supplies, particularly alcohol."},
                new DrinkFacts{Id = 5 , Fact = "Winston Churchill’s mother was the inventor of the Manhattan cocktail. It is made with whiskey and sweet vermouth."},
                new DrinkFacts{Id = 6 , Fact = "Until the mid-1600’s, wine makers in France used oil soaked rags in lieu of corks."},
                new DrinkFacts{Id = 7 , Fact = "Vikings enjoyed alcohol, but they did not drink it from a mug, a bottle or any other traditional method. Instead, they preferred to toast to their victories by imbibing their favorite alcoholic beverages from the skulls of their defeated enemies."},
                new DrinkFacts{Id = 8 , Fact = "Many historians believe that the practice of farming was not started as a means of food production. Instead, early farmers were most likely engaging in their trade in order to produce the necessary ingredients to create alcoholic beverages."},
                new DrinkFacts{Id = 9 , Fact = "So-called hangover cures date back almost as far as alcohol itself. Ancient Romans believed that eating a fried canary would take care of their hangover symptoms, and the ancient Greeks were believers in the power of cabbage. Although these so-called cures probably sound silly, keep in mind that many people today are still trying to find the perfect cure for a hangover. For example, in France they put salt into a strong cup of coffee, and in Puerto Rico some drinkers actually lift their drinking arm and rub half a lemon under it. None of these cures actually fixes any of the symptoms of a hangover"},
                new DrinkFacts{Id = 10 , Fact = "The term honeymoon traces its roots back to ancient Babylon. It was a tradition for the soon to be father-in-law to supply his daughter’s fiancé with a month’s supply of mead. This time period was referred to as the honey month, and that phrase eventually morphed into what we now call a honeymoon."},
                new DrinkFacts{Id = 11, Fact = "The phrase mind your p’s and q’s can also trace its roots back to alcohol. In England, pubs serve liquor in pint and quart sizes. If a customer became unruly, it used to be common for a bartender to tell that customer to mind their own pints and quarts. Over time, the saying was shortened and its usage was expanded."},
                new DrinkFacts{Id = 12, Fact = " In 1964, Congress declared Bourbon to be the official spirit of the United States."},
                new DrinkFacts{Id = 13, Fact = "Abraham Lincoln owned and operated several taverns, and John Hancock was a well-known alcohol dealer. President Van Buren’s mother gave birth to him in their family tavern."},
                new DrinkFacts{Id = 14, Fact = "There are 13 minerals that are essential for human life, and all of them can be found in alcohol."},
                new DrinkFacts{Id = 15, Fact = "Many people believe that there has been a worm in tequila for centuries, but that is not accurate. The drink that started this tradition was actually Mezcal, and instead of a worm, it was a Gusano butterfly caterpillar."},
                new DrinkFacts{Id = 16, Fact = "The word brandy is derived from the Dutch word brandewijn; it means burnt wine."},
                new DrinkFacts{Id = 17, Fact = "A bottle of Champagne contains approximately 49 million bubbles."},
                new DrinkFacts{Id = 18, Fact = "Drinking a glass of milk can cause a person to blow a .02 on a breathalyzer test, and that is enough to cause legal issues in some states."},
                new DrinkFacts{Id = 19, Fact = "In order to make a bottle of wine, you will need to have approximately 600 grapes on hand."},
                new DrinkFacts{Id = 20, Fact = "It is so common in Europe for teenagers to be permitted to drink that they can obtain an alcoholic beverage at the cafeteria of many high schools. It is also common throughout Europe to find alcohol on the menu at McDonalds. On the contrary, laws about teenage drinking in the U.S. are the strictest in Western civilization."}
            };

            Random rnd = new Random();

            return new UserHomeVM { DrinkFact = listOfFact[rnd.Next(listOfFact.Count)].Fact, RecentlySaved = await MethodRecentlySavedAsync(user) };
        }





        public async Task<RecentlySavedVM[]> MethodRecentlySavedAsync(ClaimsPrincipal user)
        {

            string userIdString = userManager.GetUserId(user);

            var currentUserId = winterIsComingContext.User
                .Where(u => u.IdentityUsersId == userIdString)
                .FirstOrDefault();

            var recentlySavedDrinks = winterIsComingContext
                .UserDrinkList
                .Where(u => u.KiwiUserId == currentUserId.Id)
                .ToArray();




            string j = recentlySavedDrinks[0].Apiid.ToString();
            List<Drink> temp = new List<Drink>();

            for (int i = 0; i < 4; i++)
            {
                temp.Add(new Drink { idDrink = recentlySavedDrinks[i].Apiid.ToString() });

                if (recentlySavedDrinks.Length <= temp.Count)
                    break;
            }


            var drinkById = await testRepository.GetDrinksById(temp);

            RecentlySavedVM[] recent = new RecentlySavedVM[temp.Count];

            for (int i = 0; i < drinkById.Count; i++)
            {
                recent[i] = new RecentlySavedVM { DrinkName = drinkById[i].strDrink, ImgUrl = drinkById[i].strDrinkThumb };
            }



            return recent;
        }

        public async Task<UserRecipeVM> GetRecipe(string id)
        {
            List<Drink> d = await testRepository.GetDrinks($"https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i={id}");
            Drink drink = d.First();
            return new UserRecipeVM { RecipeDrink = drink };
        }



        ////private async Task<RecentlySavedVM[]> MethodRecentlySavedAsync()
        //{
        //    var currentUser = await userManager.GetUserId();

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


        public AccountMyPageVM[] FindDrinkListByUserId(int userId)
        {

            AccountMyPageVM[] userDrink = winterIsComingContext
                .UserDrinkList
                .Where(o => o.KiwiUserId == userId)
                .Select(o => new AccountMyPageVM
                {
                    DrinkId = o.Apiid,
                })
                .ToArray();

            return userDrink;
        }

        //public UserDrinkList GetUserDrinkList(string userName)
        //{

        //    return null;
        //}
    }
}
