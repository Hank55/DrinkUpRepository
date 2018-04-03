using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkUpProject.Models.ViewModels
{
    public class UserHomeVM
    {
        public string DrinkFact { get; set; }
        public SearchVM SearchForm { get; set; }
        public RecentlySavedVM[] RecentlySaved { get; set; }
        public GuestIndexLogInVM LoggedInAs { get; set; }

        public string UserName { get; set; }
    }
}
