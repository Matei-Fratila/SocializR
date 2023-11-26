using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocializR.Entities;

namespace SocializR.DataAccess.Configuration
{
    internal class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.ToTable("Friendships");

            builder.HasKey(e => new { e.FirstUserId, e.SecondUserId });

            builder.Property(e => e.FirstUserId).HasColumnName("FirstUserId");

            builder.Property(e => e.SecondUserId).HasColumnName("SecondUserId");

            builder.Property(e => e.CreatedDate).HasColumnType("datetime");

            builder.HasOne(d => d.FirstUser)
                .WithMany(p => p.FriendsFirstUser)
                .HasForeignKey(d => d.FirstUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Friends_Users2");

            builder.HasOne(d => d.SecondUser)
                .WithMany(p => p.FriendsSecondUser)
                .HasForeignKey(d => d.SecondUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Friends_Users3");
        }
    }
}
