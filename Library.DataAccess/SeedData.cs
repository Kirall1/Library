using System.Security.Claims;
using Library.Domain;
using Library.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess
{
    public static class SeedData
    {
        public static async Task SeedDatabaseAsync(DatabaseContext context,
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                await context.Database.MigrateAsync();
            }

            if (!context.Users.Any())
            {
                var newUser = new ApplicationUser()
                {
                    UserName = "Admin",
                    Email = "Admin@mail.com"
                };
                await userManager.CreateAsync(newUser, "Admin_123");
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, newUser.UserName),
                    new Claim(ClaimTypes.Email, newUser.Email),
                    new Claim(ClaimTypes.Role, "Admin")
                };
                var addedUser = await userManager.FindByNameAsync(newUser.UserName);
                await userManager.AddClaimsAsync(addedUser, claims);

            }

            if (!context.Authors.Any())
            {
                await context.Authors.AddAsync(new Author()
                {
                    Name = "Lev",
                    Surname = "Tolstoy",
                    BirthDate = new DateOnly(1828, 9, 9),
                    Country = "Russia"
                });
                await context.SaveChangesAsync();
            }

            if (!context.Books.Any())
            {
                await context.Books.AddAsync(new Book()
                {
                    Isbn = "9780192833983",
                    Title = "War and Peace",
                    Genre = "Novel",
                    Description = "War and Peace broadly focuses on Napoleon's invasion of Russia in 1812.",
                    Author = await unitOfWork.Authors.GetFirstByPredicateAsync(a =>
                        a.Name == "Lev" && a.Surname == "Tolstoy"),
                    Image = "1188cfdb-6676-4c90-bd4d-a3037c2e591c.jpg"
                });
                await context.SaveChangesAsync();
            }
        }
    }
}
