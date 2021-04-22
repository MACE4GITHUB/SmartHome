using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHome.Configuration.Infrastructure.Models;

namespace SmartHome.Configuration.Infrastructure.EntityConfigurations
{
    public class SensorDbEntityTypeConfiguration : IEntityTypeConfiguration<SensorDb>
    {
        public void Configure(EntityTypeBuilder<SensorDb> builder)
        {
            builder.ToTable("Sensor", ConfiguringContext.DefaultSchema);

            builder.HasKey(s => s.SensorId);

            builder.Property(s => s.SensorTypeId)
                .IsRequired();

            builder.Property(s => s.Name)
                .HasMaxLength(256)
                .IsUnicode();

            builder.Property(s => s.Description)
                .HasMaxLength(1024)
                .IsUnicode();

            builder.Property(s => s.Precision)
                .IsRequired()
                .HasDefaultValue(4);

            builder.Property(s => s.IsEnabled)
                .IsRequired();
        }
    }
}
