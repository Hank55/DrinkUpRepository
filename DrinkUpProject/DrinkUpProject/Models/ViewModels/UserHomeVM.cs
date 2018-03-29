using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkUpProject.Models.ViewModels
{
    public class UserHomeVM : ITestVM
    {
        public string DrinkFact { get; set; }
        public SearchVM SearchForm { get; set; }
    }
}
