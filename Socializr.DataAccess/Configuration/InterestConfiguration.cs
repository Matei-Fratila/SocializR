namespace SocializR.DataAccess.Configuration;

internal class InterestConfiguration : IEntityTypeConfiguration<Interest>
{
    public void Configure(EntityTypeBuilder<Interest> builder)
    {
        builder.ToTable("Interests");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).HasColumnName("Id");

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.ParentId).HasColumnName("ParentId");

        builder.HasOne(d => d.Parent)
            .WithMany(p => p.ChildInterests)
            .HasForeignKey(d => d.ParentId)
            .HasConstraintName("FK_Interests_Interests")
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired(false);
    }
}
