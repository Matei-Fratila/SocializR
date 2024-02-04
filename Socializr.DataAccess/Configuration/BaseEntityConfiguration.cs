using Socializr.Models.Entities.Base;

namespace SocializR.DataAccess.Configuration;
internal class BaseEntityConfiguration : IEntityTypeConfiguration<BaseEntity>
{
    public void Configure(EntityTypeBuilder<BaseEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).HasColumnName("Id");

        builder.Property(e => e.CreatedDate).HasColumnType("datetime");

        builder.Property(e => e.LastModifiedDate).HasColumnType("datetime");
    }
}
