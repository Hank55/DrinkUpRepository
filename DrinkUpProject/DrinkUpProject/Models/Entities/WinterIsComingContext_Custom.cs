using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DrinkUpProject.Models.Entities
{
    public partial class WinterIsComingContext : DbContext
    {
        public WinterIsComingContext(DbContextOptions<WinterIsComingContext> options) : base(options)
        {

        }
    }
}
