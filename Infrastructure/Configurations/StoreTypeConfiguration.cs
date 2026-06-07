using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class StoreTypeConfiguration : IEntityTypeConfiguration<StoreType>
{
    public void Configure(EntityTypeBuilder<StoreType> builder)
    {
        builder.HasKey(st => st.Id);

        builder.Property(st => st.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(st => st.Name)
            .IsUnique();
    }
}
