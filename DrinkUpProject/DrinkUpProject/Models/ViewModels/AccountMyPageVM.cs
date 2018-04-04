using DrinkUpProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkUpProject.Models.ViewModels
{
    public class AccountMyPageVM
    {
        public RecentlySavedVM[] RecentlySaved { get; set; }
        public string DrinkId { get; set; }



    }
}
