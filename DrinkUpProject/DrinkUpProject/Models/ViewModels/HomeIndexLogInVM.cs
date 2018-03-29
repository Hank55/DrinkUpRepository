using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkUpProject.Models.ViewModels
{
    [Bind(Prefix = nameof(HomeIndexVM.LogInForm))]
    public class HomeIndexLogInVM
    {
        [Display(Name ="Username")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
