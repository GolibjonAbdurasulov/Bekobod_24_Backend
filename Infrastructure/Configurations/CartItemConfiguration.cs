using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable("CartItems");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Price)
            .HasPrecision(18, 2);

        builder.Property(x => x.Quantity)
            .HasDefaultValue(1);

        builder.Property(x => x.BookingTime)
            .IsRequired(false);

        builder.HasOne<Cart>()
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.CartId);
    }
}