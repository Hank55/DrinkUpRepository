using DrinkUpProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkUpProject.Models.ViewModels
{
    public class AccountMyPageAsyncVM
    {
        public List<Drink> DrinkList { get; set; }
        public ICollection<UserDrinkList> UserDrinkList { get; set; }
    }
}
