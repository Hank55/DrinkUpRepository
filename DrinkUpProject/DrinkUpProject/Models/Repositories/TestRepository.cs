using DrinkUpProject.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DrinkUpProject.Models.Repositories
{
    public class TestRepository
    {
        public static List<User> users;
           public static List<HomeResultVM[]> searchResultListings = new List<HomeResultVM[]>();
        // ... Use HttpClient.
        static HttpClient client = new HttpClient();

        public TestRepository()
        {
            users = new List<User>
            {
                new User
                {
                    FirstName= "Hanna",
                    LastName = "Pawahi",
                    Email = "hanna.pawahi@test.se",
                    Password = "hannaebäst",
                    FavouriteDrink="Vatten och saft",
                    UserListDrinkId = new List<string>
                    {
                        "13427"
                    }
                },

                new User
                {
                    FirstName= "Åsa",
                    LastName = "Tysk",
                    Email = "asa.tysk@test.se",
                    Password = "åsaeoxåbra",
                    FavouriteDrink="Tequila",
                     UserListDrinkId = new List<string>
                    {
                        "13060"
                    }
                }
            };
        }

        public async Task<List<Drink>> GetDrinks(string searchURL)
        {
            HttpResponseMessage response = await client.GetAsync(searchURL);
            HttpContent content = response.Content; 
            
                // ... Read the string.
            string result = await content.ReadAsStringAsync();

            DrinkDirectory d = JsonConvert.DeserializeObject<DrinkDirectory>(result);

            return d.drinks;
        }

        internal async Task<HomeResultVM[]> SearchResult(string searchURL)
        {
            List<Drink> drinkList = await GetDrinks(searchURL);

            HomeResultVM[] listResults = new HomeResultVM[drinkList.Count];

            for (int i = 0; i < listResults.Length; i++)
            {
                listResults[i] = new HomeResultVM { DrinkName = drinkList[i].strDrink, DrinkImg = drinkList[i].strDrinkThumb };
            }

            SaveToSearchResultList(listResults);

            return listResults;
             
        }

        private void SaveToSearchResultList(HomeResultVM[] listResults)
        {
        }

        internal async Task<HomeResultVM[]> SearchResultIngredient(string ingredient)
        {


            string searchURL = $"https://www.thecocktaildb.com/api/json/v1/1/filter.php?i={ingredient}";

            

            return await SearchResult(searchURL);

        }

        internal async Task<HomeResultVM[]> SearchResultDrinkName(string drinkName)
        {
            string searchURL = $"https://www.thecocktaildb.com/api/json/v1/1/search.php?s={drinkName}";

            return await SearchResult(searchURL);
        }

        public async Task<Drink> GetRandomDrink()
        {
            List<Drink> drinkList = await GetDrinks("https://www.thecocktaildb.com/api/json/v1/1/random.php");
            return drinkList.First();
        }


    }
}
