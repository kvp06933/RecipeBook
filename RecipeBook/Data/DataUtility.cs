using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using RecipeBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Data
{
    public class DataUtility
    {
        //private static int book1Id;
        //private static int book2Id;

        public static string GetConnectionString(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            return string.IsNullOrEmpty(databaseUrl) ? connectionString : BuildConnectionString(databaseUrl);
        }

        private static string BuildConnectionString(string databaseUrl)
        {
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/'),
                SslMode = SslMode.Prefer,
                TrustServerCertificate = true
            };
            return builder.ToString();
        }

        public static async Task ManageDataAsync(IHost host)
        {
            using var svcScope = host.Services.CreateScope();
            var svcProvider = svcScope.ServiceProvider;
            var dbContextSvc = svcProvider.GetRequiredService<ApplicationDbContext>();
            var userManagerSvc = svcProvider.GetRequiredService<UserManager<RBUser>>();
            await dbContextSvc.Database.MigrateAsync();

            //    //Things we need to create:
            //    //Roles


            //    //Users
            //    await SeedUsersAsync(userManagerSvc);
            //    //Books
            //    await SeedBooksAsync(dbContextSvc);
            //    await SeedRecipesAsync(dbContextSvc);
            //    await SeedIngredientsAsync(dbContextSvc);
            //    await SeedInstructionalStepsAsync(dbContextSvc);

            //}


            //private static async Task SeedBooksAsync(ApplicationDbContext dbContextSvc)
            //{
            //    try
            //    {
            //        IList<Book> defaultBooks = new List<Book>()
            //        {
            //            new Book() {Name = "Book 1", Description = "This is default Book 1"},
            //            new Book() {Name = "Book 2", Description = "This is default Book 2"},

            //        };

            //        var dbCompanies = dbContextSvc.Books.Select(c => c.Name).ToList();

            //        await dbContextSvc.Books.AddRangeAsync(defaultBooks.Where(c => !dbCompanies.Contains(c.Name)));
            //        await dbContextSvc.SaveChangesAsync();

            //        book1Id = dbContextSvc.Books.FirstOrDefault(c => c.Name == "Book 1").Id;
            //        book2Id = dbContextSvc.Books.FirstOrDefault(c => c.Name == "Book 2").Id;

            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("******** ERROR ********");
            //        Console.WriteLine("Error Seeding Books");
            //        Console.WriteLine(ex.Message);
            //        Console.WriteLine("***********************");
            //    }

            //}

            //private static async Task SeedUsersAsync(UserManager<RBUser> userManagerSvc)
            //{
            //    var defaultUser = new RBUser
            //    {
            //        Email = "kpitts@mailinator.com",
            //        UserName = "kpitts@mailinator.com",
            //        FirstName = "Katherine",
            //        LastName = "Pitts",
            //        PhoneNumber = "555-1212",
            //        EmailConfirmed = true,

            //    };
            //    try
            //    {
            //        var user = await userManagerSvc.FindByEmailAsync(defaultUser.Email);
            //        if (user is null)
            //        {
            //            await userManagerSvc.CreateAsync(defaultUser, "Abc&123!");

            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("******** ERROR ********");
            //        Console.WriteLine("Error Seeding RecipeBook User 1");
            //        Console.WriteLine(ex.Message);
            //        Console.WriteLine("***********************");
            //    }


            //}

            //private static async Task SeedRecipesAsync(ApplicationDbContext dbContextSvc)
            //{
            //    var defaultRecipe = new Recipe
            //    {
            //        Title="Creamy Herb Chicken",
            //        Description = "Creamy Herb Chicken with Broccolli",
            //        Content=""

            //    };
            //    try
            //    {
            //        var user = await userManagerSvc.FindByEmailAsync(defaultUser.Email);
            //        if (user is null)
            //        {
            //            await userManagerSvc.CreateAsync(defaultUser, "Abc&123!");

            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("******** ERROR ********");
            //        Console.WriteLine("Error Seeding RecipeBook User 1");
            //        Console.WriteLine(ex.Message);
            //        Console.WriteLine("***********************");
            //    }
            //}

            //private static async Task SeedIngredientsAsync(ApplicationDbContext dbContextSvc)
            //{
            //    throw new NotImplementedException();
            //}

            //private static async Task SeedInstructionalStepsAsync(ApplicationDbContext dbContextSvc)
            //{
            //    throw new NotImplementedException();
            //}
        }
    }
}
