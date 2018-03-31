using System;
using System.Collections.Generic;

namespace DrinkUpProject.Models.Entities
{
    public partial class User
    {
        public User()
        {
            UserDrinkList = new HashSet<UserDrinkList>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FavDrink { get; set; }
        public string IdentityUsersId { get; set; }

        public ICollection<UserDrinkList> UserDrinkList { get; set; }
    }
}
