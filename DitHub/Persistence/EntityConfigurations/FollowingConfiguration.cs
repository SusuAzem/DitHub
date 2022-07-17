using DitHub.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DitHub.Persistence.EntityConfigurations
{
    public class FollowingConfiguration : IEntityTypeConfiguration<Following>
    {
        public void Configure(EntityTypeBuilder<Following> builder)
        {
            builder.HasKey(f => new { f.FollowerId, f.FolloweeId });

            builder.HasOne(f => f.Follower)
                   .WithMany(a => a.Followees)
                   .HasForeignKey(f => f.FollowerId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(f => f.Followee)
                   .WithMany(a => a.Followers)
                   .HasForeignKey(f => f.FolloweeId)
                   .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
