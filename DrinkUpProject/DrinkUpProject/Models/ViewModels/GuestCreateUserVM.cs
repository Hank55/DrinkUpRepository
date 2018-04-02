using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkUpProject.Models.ViewModels
{
    public class GuestCreateUserVM
    {

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please enter your first name.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your email.")]
        [EmailAddress(ErrorMessage = "The format of the email address is incorrect.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter username.")]
        [StringLength(200, ErrorMessage = "Please enter a username.", MinimumLength = 2)]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter your favorite drink.")]
        [StringLength(200, ErrorMessage = "Please enter your favorite drink.", MinimumLength = 3)]
        [Display(Name = "Favorite Drink")]
        public string FavouriteDrink { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        //[StringLength(200, ErrorMessage = "Please enter a password", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please re-enter your password")]
        [Display(Name = "Re-Enter Password")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ReEnterPassword { get; set; }
    }
}
