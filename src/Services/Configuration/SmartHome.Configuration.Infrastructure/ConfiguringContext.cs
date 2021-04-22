using Microsoft.EntityFrameworkCore;
using SmartHome.Configuration.Infrastructure.EntityConfigurations;
using SmartHome.Configuration.Infrastructure.Extentions;
using SmartHome.Configuration.Infrastructure.Models;

namespace SmartHome.Configuration.Infrastructure
{
    public class ConfiguringContext : DbContext
    {
        public static readonly string DefaultSchema = "configuring";

        public DbSet<SensorDb> Sensors { get; set; }

        public DbSet<SensorTypeDb> SensorTypes { get; set; }

        public ConfiguringContext(DbContextOptions<ConfiguringContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SensorDbEntityTypeConfiguration).Assembly);
            modelBuilder.Seed();
        }
    }
}
