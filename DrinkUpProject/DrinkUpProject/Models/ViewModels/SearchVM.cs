﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkUpProject.Models.ViewModels
{

    public class SearchVM
    {
        public string SearchItem { get; set; }
        public string ParamUrl { get; set; }
    }
}
