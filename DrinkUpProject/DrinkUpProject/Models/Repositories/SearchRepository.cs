﻿using DrinkUpProject.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DrinkUpProject.Models.Repositories
{
    public class SearchRepository
    {
        public static List<GuestResultVM[]> searchResultListings = new List<GuestResultVM[]>();
        // ... Use HttpClient.
        //static HttpClient client = new HttpClient(); Avkommentera när testrepository töms


        public async Task<List<Drink>> GetDrinks(string searchURL)
        {
            HttpResponseMessage response = await TestRepository.client.GetAsync(searchURL);
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
                listResults[i] = new GuestResultVM { DrinkName = drinkList[i].strDrink, DrinkImg = drinkList[i].strDrinkThumb, DrinkInfoShort = ToShortInfo(drinkList[i].strInstructions) };
            }

            SaveToSearchResultList(listResults);

            return listResults;

        }

        private async Task<List<Drink>> GetDrinksById(List<Drink> drinkList)
        {
            var tempArray = new List<Drink>();

            for (int i = 0; i < drinkList.Count; i++)
            {
                var findDrinkByIdURL = $"https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i={drinkList[i].idDrink}";
                var testListDrink = await GetDrinks(findDrinkByIdURL);
                tempArray.Add(new Drink { strDrink = testListDrink[0].strDrink, strDrinkThumb = testListDrink[0].strDrinkThumb, strInstructions = testListDrink[0].strInstructions });
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

        public async Task<Drink> GetRecipe ()
        {
            List<Drink> drinkList = await GetDrinks("https://www.thecocktaildb.com/api/json/v1/1/random.php");
            return drinkList.Single();
        }
    }
}
