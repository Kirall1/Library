using Library.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess
{
    public static class SeedData
    {
        public static async Task SeedDatabaseAsync(DatabaseContext context, UserManager<User> userManager, CancellationToken cancellationToken = default)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                await context.Database.MigrateAsync(cancellationToken);
            }
        }
    }
}
