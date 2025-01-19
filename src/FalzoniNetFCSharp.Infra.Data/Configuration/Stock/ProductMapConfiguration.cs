using FalzoniNetFCSharp.Domain.Entities.Stock;
using System.Data.Entity.ModelConfiguration;

namespace FalzoniNetFCSharp.Infra.Data.Configuration.Stock
{
    public class ProductMapConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductMapConfiguration()
        {
            HasKey(p => p.Id);

            Property(p => p.CategoryId).IsRequired();

            Property(p => p.Name).IsRequired().HasMaxLength(500);

            Property(p => p.Code).IsRequired();

            Property(p => p.Description).HasMaxLength(500).HasColumnType("text");

            Property(p => p.Price).IsRequired();

            Property(p => p.Created).IsRequired();

            Property(p => p.Modified).IsOptional();


            HasRequired(p => p.Category).WithRequiredPrincipal().Map(f => f.MapKey("CategoryId"));
        }
    }
}

