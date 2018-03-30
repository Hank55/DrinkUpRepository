using System;
using System.Collections.Generic;

namespace DrinkUpProject.Models.Entities
{
    public partial class UserDrinkList
    {
        public int Id { get; set; }
        public string Apiid { get; set; }
        public int KiwiUserId { get; set; }

        public User KiwiUser { get; set; }
    }
}
