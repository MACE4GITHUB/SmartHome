using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHome.Configuration.Infrastructure.Models;

namespace SmartHome.Configuration.Infrastructure.EntityConfigurations
{
    public class SensorTypeDbEntityTypeConfiguration : IEntityTypeConfiguration<SensorTypeDb>
    {
        public void Configure(EntityTypeBuilder<SensorTypeDb> builder)
        {
            builder.ToTable("SensorType", ConfiguringContext.DefaultSchema);

            builder.HasKey(s => s.SensorTypeId);

            builder.Property(s => s.TypeName)
                .HasMaxLength(256)
                .IsUnicode();

            builder.Property(s => s.TypeDescription)
                .HasMaxLength(1024)
                .IsUnicode();

            builder.Property(s => s.MinValue)
                .IsRequired()
                .HasColumnType("decimal")
                .HasPrecision(18, 7);

            builder.Property(s => s.MinNormalValue)
                .IsRequired()
                .HasColumnType("decimal")
                .HasPrecision(18, 7);

            builder.Property(s => s.MaxValue)
                .IsRequired()
                .HasColumnType("decimal")
                .HasPrecision(18, 7);

            builder.Property(s => s.MaxNormalValue)
                .IsRequired()
                .HasColumnType("decimal")
                .HasPrecision(18, 7);
        }
    }
}
