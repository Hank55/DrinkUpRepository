using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DrinkUpProject.Models.Entities
{
    public partial class WinterIsComingContext : DbContext
    {
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserDrinkList> UserDrinkList { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Data Source=springiscoming.database.windows.net;Initial Catalog=WinterIsComing;Integrated Security=False;User ID=Hank;Password=Viliggerbratill!;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "kiwi");

                entity.Property(e => e.FavDrink)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IdentityUsersId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UserDrinkList>(entity =>
            {
                entity.ToTable("UserDrinkList", "kiwi");

                entity.Property(e => e.Apiid)
                    .IsRequired()
                    .HasColumnName("APIId")
                    .HasMaxLength(10);

                entity.HasOne(d => d.KiwiUser)
                    .WithMany(p => p.UserDrinkList)
                    .HasForeignKey(d => d.KiwiUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserDrinkList_ToTable");
            });
        }
    }
}
