using System.Diagnostics.CodeAnalysis;
using donet_rpg.Models;
using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace donet_rpg.Data
{
    public class DataContext: DbContext
    {
       

        public DataContext( DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Character> Characters {get;set;}
           public DbSet<User> Users {get;set;}
    }
}