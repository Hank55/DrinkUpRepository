﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkUpProject.Models.ViewModels
{
    public class HomeResultVM
    {
        public HomeIndexLogInVM LoggedInAs { get; set; }
        public string DrinkName { get; set; }
        public string DrinkImg { get; set; }
        public string DrinkInfoShort { get; set; }
    }
}
