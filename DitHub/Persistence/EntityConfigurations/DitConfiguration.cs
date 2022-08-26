using DitHub.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DitHub.Persistence.EntityConfigurations
{
    public class DitConfiguration : IEntityTypeConfiguration<Dit>
    {
        public void Configure(EntityTypeBuilder<Dit> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.AppUserId).IsRequired();
            builder.Property(d => d.Date).IsRequired();
            builder.Property(d => d.Venue).IsRequired().HasMaxLength(255);
            builder.Property(d => d.GenreId).IsRequired().HasDefaultValue(1);

            builder.HasMany(d => d.Notifications)
                .WithOne(n => n.Dit)
                .HasForeignKey(n => n.DitId)
                .IsRequired();

        }
    }
}
