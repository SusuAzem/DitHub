using DitHub.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DitHub.Persistence.EntityConfigurations
{
    public class FaveDitConfiguration : IEntityTypeConfiguration<FaveDit>
    {
        public void Configure(EntityTypeBuilder<FaveDit> builder)
        {
            builder.HasKey(f => new { f.AppUserId, f.DitId });

            builder.HasOne(f => f.AppUser)
                .WithMany(a => a.FaveDits)
                .HasForeignKey(f => f.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(f => f.Dit)
                .WithMany(d => d.FaveDits)
                .HasForeignKey(f => f.DitId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
