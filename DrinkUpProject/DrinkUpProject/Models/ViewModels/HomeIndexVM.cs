﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkUpProject.Models.ViewModels
{
    public class HomeIndexVM: ITestVM
    {

        public SearchVM SearchForm { get; set; }
        public HomeIndexLogInVM LogInForm{ get; set;}

    }
}
