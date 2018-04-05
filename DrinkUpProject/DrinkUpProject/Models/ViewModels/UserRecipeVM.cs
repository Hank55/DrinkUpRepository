using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkUpProject.Models.ViewModels
{
    public class UserRecipeVM
    {
        public GuestIndexLogInVM LoggedInAs { get; set; }
        public SearchVM SearchForm { get; set; }
        public Drink RecipeDrink { get; set; }
        public string IsInList { get; set; }
    }
}
