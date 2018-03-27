﻿using DrinkUpProject.Models.ViewModels;
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
        // ... Use HttpClient.
        static HttpClient client = new HttpClient();

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

            return listResults;
             
        }

        internal async Task<HomeResultVM[]> SearchResultIngredient()
        {
            string searchURL = "https://www.thecocktaildb.com/api/json/v1/1/filter.php?i=Vodka";

            return await SearchResult(searchURL);
        }

        internal async Task<HomeResultVM[]> SearchResultDrinkName()
        {
            string searchURL = "https://www.thecocktaildb.com/api/json/v1/1/search.php?s=margarita";

            return await SearchResult(searchURL);
        }

        public async Task<Drink> GetRandomDrink()
        {
            List<Drink> drinkList = await GetDrinks("https://www.thecocktaildb.com/api/json/v1/1/random.php");
            return drinkList.First();
        }


    }
}