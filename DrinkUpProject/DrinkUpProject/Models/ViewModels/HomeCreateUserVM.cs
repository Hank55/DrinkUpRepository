using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkUpProject.Models.ViewModels
{
    public class HomeCreateUserVM
    {
        [Display(Name = "First Name")]
        [StringLength(200, ErrorMessage = "Please enter a first name.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name.")]
        [Display(Name = "Last Name")]
        [StringLength(200, ErrorMessage = "Please enter a last name.", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your email.")]
        [EmailAddress(ErrorMessage = "The format of the email address is incorrect.")]
        public string Email { get; set; }

        [StringLength(200, ErrorMessage = "Please enter a username.", MinimumLength = 2)]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [StringLength(200, ErrorMessage = "Please enter your favorite drink.", MinimumLength = 3)]
        [Display(Name = "Favorite Drink")]
        public string FavoriteDrink { get; set; }

        [StringLength(200, ErrorMessage = "Please enter a password", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please re-enter your password")]
        [Display(Name = "Re-Enter Password")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ReEnterPassword { get; set; }
    }
}
