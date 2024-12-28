using CashHub.Models;
using Microsoft.EntityFrameworkCore;

namespace CashHub.Data
{
    public class CashHubContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Ads> ads { get; set; }
        public CashHubContext(DbContextOptions options) : base(options)
        {

        }

    }
}
