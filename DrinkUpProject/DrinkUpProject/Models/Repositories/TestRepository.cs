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

        public static List<GuestResultVM[]> searchResultListings = new List<GuestResultVM[]>();
        // ... Use HttpClient.
        public static HttpClient client = new HttpClient();

        public TestRepository()
        {
        }

        public async Task<List<Drink>> GetDrinks(string searchURL)
        {
            HttpResponseMessage response = await client.GetAsync(searchURL);
            HttpContent content = response.Content;

            // ... Read the string.
            string result = await content.ReadAsStringAsync();

            DrinkDirectory d = JsonConvert.DeserializeObject<DrinkDirectory>(result);
            if (d == null || d.drinks == null)
            {
                d = new DrinkDirectory();
                d.drinks = new List<Drink> { new Drink { strDrink = "NoSearchResult" } };
            }

            return d.drinks;
        }


        internal async Task<GuestResultVM[]> SearchResult(string searchURL)
        {
            List<Drink> drinkList = await GetDrinks(searchURL);


            if (drinkList[0].strDrink != "NoSearchResult" && searchURL.Contains("https://www.thecocktaildb.com/api/json/v1/1/filter.php?i="))
                drinkList = await GetDrinksById(drinkList);

            GuestResultVM[] listResults = new GuestResultVM[drinkList.Count];

            for (int i = 0; i < listResults.Length; i++)
            {
                listResults[i] = new GuestResultVM { DrinkName = drinkList[i].strDrink, DrinkImg = drinkList[i].strDrinkThumb, DrinkInfoShort = ToShortInfo(drinkList[i].strInstructions), DrinkId = drinkList[i].idDrink };
            }

            SaveToSearchResultList(listResults);

            return listResults;

        }

        public async Task<List<Drink>> GetDrinksById(List<Drink> drinkList)
        {
            var tempArray = new List<Drink>();

            for (int i = 0; i < drinkList.Count; i++)
            {
                var findDrinkByIdURL = $"https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i={drinkList[i].idDrink}";
                var tempListDrink = await GetDrinks(findDrinkByIdURL);
                Drink drink = tempListDrink[0];
                tempArray.Add(new Drink
                { strDrink = drink.strDrink,
                    strDrinkThumb = drink.strDrinkThumb,
                    strInstructions = drink.strInstructions,
                    strIngredient1 = drink.strIngredient1,
                    strIngredient2 = drink.strIngredient2,
                    strIngredient3 = drink.strIngredient3,
                    strIngredient4 = drink.strIngredient4,
                    strIngredient5 = drink.strIngredient5,
                    strIngredient6 = drink.strIngredient6,
                    strIngredient7 = drink.strIngredient7,
                    strIngredient8 = drink.strIngredient8,
                    strIngredient9 = drink.strIngredient9,
                    strIngredient10 = drink.strIngredient10,
                    strIngredient11 = drink.strIngredient11,
                    strIngredient12 = drink.strIngredient12,
                    strIngredient13 = drink.strIngredient13,
                    strIngredient14 = drink.strIngredient14,
                    strIngredient15 = drink.strIngredient15,
                    strMeasure1 = drink.strMeasure1,
                    strMeasure2 = drink.strMeasure2,
                    strMeasure3 = drink.strMeasure3,
                    strMeasure4 = drink.strMeasure4,
                    strMeasure5 = drink.strMeasure5,
                    strMeasure6 = drink.strMeasure6,
                    strMeasure7 = drink.strMeasure7,
                    strMeasure8 = drink.strMeasure8,
                    strMeasure9 = drink.strMeasure9,
                    strMeasure10 = drink.strMeasure10,
                    strMeasure11 = drink.strMeasure11,
                    strMeasure12 = drink.strMeasure12,
                    strMeasure13 = drink.strMeasure13,
                    strMeasure14 = drink.strMeasure14,
                    strMeasure15 = drink.strMeasure15,
                    strIBA = drink.strIBA,
                    strAlcoholic = drink.strAlcoholic,
                    strCategory = drink.strCategory,
                    strGlass = drink.strGlass,
                    dateModified = drink.dateModified,
                    idDrink= drink.idDrink
                });
            }

            return tempArray;
        }

        private string ToShortInfo(string strInstructions)
        {
            var shortInfo = "";
            if (!String.IsNullOrWhiteSpace(strInstructions))
            {
                var splitInfo = strInstructions.Split(" ");
                if (splitInfo.Length > 6)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        shortInfo += splitInfo[i] + " ";
                    }
                }
                else
                    shortInfo = "Short description in bio";
            }
            else
                shortInfo = "This recipe do not have a description";

            return shortInfo += "...";
        }

        private void SaveToSearchResultList(GuestResultVM[] listResults)
        {
            searchResultListings.Add(listResults);
        }

        public GuestResultVM[] GetLastSearchResult()
        {
            var drinks = searchResultListings.LastOrDefault();
            return drinks;
        }

        internal async Task<GuestResultVM[]> SearchResultIngredient(string ingredient)
        {


            string searchURL = $"https://www.thecocktaildb.com/api/json/v1/1/filter.php?i={ingredient}";



            return await SearchResult(searchURL);

        }

        internal async Task<GuestResultVM[]> SearchResultDrinkName(string drinkName)
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

