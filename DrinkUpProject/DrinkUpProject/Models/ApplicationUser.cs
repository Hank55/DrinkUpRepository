using Microsoft.AspNetCore.Identity;

namespace DrinkUpProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FavDrink { get; set; }

    }
}
