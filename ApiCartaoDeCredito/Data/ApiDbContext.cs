using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCartaoDeCredito.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .Property(b => b.Email)
                .HasMaxLength(320); //Maximum email length according to RFC 3696

            modelBuilder.Entity<Person>()
                .HasData(
                    new Person { PersonId = 1, Email = "a@gmail.com" },
                    new Person { PersonId = 2, Email = "b@gmail.com" }
                );

            modelBuilder.Entity<CreditCard>()
               .HasData(
                    new CreditCard
                    {
                        CreditCardId = 2,
                        IsActive = false,
                        Number = "4916426552028583", //Credit card must be activated later by the user
                        CVV = "457",
                        ExpiryDate = "01/25",
                        DateOfCreation = DateTime.Now.AddYears(-2),
                        PersonId = 1
                    }
               );
        }

    }
}
