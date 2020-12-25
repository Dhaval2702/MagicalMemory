using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApi.Entities;

namespace WebApi.Helpers
{
    public class DataContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public DbSet<countries> countries { get; set; }

        public DbSet<states> states { get; set; }

        public DbSet<DependentChildren> DependentChildren { get; set; }

        public DbSet<ChildPhotoMemory> ChildPhotoMemory { get; set; }

        public DbSet<ChildWeightDetail> ChildWeightDetail { get; set; }


        private readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
            options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
        }
    }
}