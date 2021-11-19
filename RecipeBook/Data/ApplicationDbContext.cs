using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBook.Data
{
    public class ApplicationDbContext : IdentityDbContext<RBUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<InstructionalStep> InstructionalSteps { get; set; }
    }
}
