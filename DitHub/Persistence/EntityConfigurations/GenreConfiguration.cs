using DitHub.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DitHub.Persistence.EntityConfigurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Id)
                   .IsRequired();

            builder.Property(g => g.Name)
                   .IsRequired()
                   .HasMaxLength(255)
                   .HasDefaultValue(string.Empty);

            builder.HasMany(g => g.Dits)
                   .WithOne(d => d.Genre)
                   .HasForeignKey(d => d.GenreId)
                   .OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
