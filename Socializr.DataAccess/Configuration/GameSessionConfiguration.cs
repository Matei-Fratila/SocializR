using Socializr.Models.Entities;

namespace SocializR.DataAccess.Configuration;
internal class GameSessionConfiguration : IEntityTypeConfiguration<GameSession>
{
    public void Configure(EntityTypeBuilder<GameSession> builder)
    {
        builder.ToTable("GameSessions");

        builder.HasOne(e => e.User)
            .WithMany(e => e.GameSessions)
            .HasForeignKey(e => e.UserId)
            .HasConstraintName("FK_GameSessions_Users");
    }
}
