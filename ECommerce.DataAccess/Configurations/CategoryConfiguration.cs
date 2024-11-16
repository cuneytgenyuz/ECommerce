using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ECommerce.Entities.Entities.Concrete;

namespace ECommerce.DataAccess.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        // Primary Key
        builder.HasKey(c => c.Id);

        // Id Property
        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        // Name Property
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(50);

        // Description Property
        builder.Property(c => c.Description)
            .HasMaxLength(250)
            .IsRequired(false); 

        // IsDeleted Property
        builder.Property(c => c.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        // CreatedUserId Property
        builder.Property(c => c.CreatedUserId)
            .IsRequired();

        // CreatedDate Property
        builder.Property(c => c.CreatedDate)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()");

        // UpdatedUserId Property
        builder.Property(c => c.UpdatedUserId)
            .IsRequired(false);

        // UpdatedDate Property
        builder.Property(c => c.UpdatedDate)
            .IsRequired(false);

        // Relationships
        builder.HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}