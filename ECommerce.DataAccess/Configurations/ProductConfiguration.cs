using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ECommerce.Entities.Entities.Concrete;

namespace ECommerce.DataAccess.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // Primary Key
        builder.HasKey(p => p.Id);

        // Id Property
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        // Name Property
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        // Price Property
        builder.Property(p => p.Price)
            .HasColumnType("decimal(18,2)");

        // Stock Property
        builder.Property(p => p.Stock)
            .HasDefaultValue(0);

        // IsDeleted Property
        builder.Property(p => p.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        // CreatedUserId Property
        builder.Property(p => p.CreatedUserId)
            .IsRequired();

        // CreatedDate Property
        builder.Property(p => p.CreatedDate)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()"); 

        // UpdatedUserId Property
        builder.Property(p => p.UpdatedUserId)
            .IsRequired(false); 

        // UpdatedDate Property
        builder.Property(p => p.UpdatedDate)
            .IsRequired(false); 

        // Relationships
        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}