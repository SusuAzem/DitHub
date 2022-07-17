using DitHub.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DitHub.Persistence.EntityConfigurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasMany(a => a.Dits)
                .WithOne(d => d.AppUser)
                .HasForeignKey(d => d.AppUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(a => a.Name)
                .HasDefaultValue(string.Empty)
                .IsRequired()
                .HasMaxLength(100);

            builder.ToTable("AspNetUsers");
        }
    }
}
