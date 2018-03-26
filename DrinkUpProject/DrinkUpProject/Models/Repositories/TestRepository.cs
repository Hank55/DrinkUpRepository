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

        public async Task<Drink> GetRandomDrink()
        {
            List<Drink> drinkList = await GetDrinks("https://www.thecocktaildb.com/api/json/v1/1/random.php");
            return drinkList.First();
        }
    }
}
