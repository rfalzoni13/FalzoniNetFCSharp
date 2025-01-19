using FalzoniNetFCSharp.Domain.Entities.Stock;
using System.Data.Entity.ModelConfiguration;

namespace FalzoniNetFCSharp.Infra.Data.Configuration.Stock
{
    public class ProductCategoryMapConfiguration : EntityTypeConfiguration<ProductCategory>
    {
        public ProductCategoryMapConfiguration()
        {
            HasKey(p => p.Id);

            Property(p => p.Name).IsRequired().HasMaxLength(512);

            Property(p => p.Created).IsRequired();

            Property(p => p.Modified).IsOptional();
        }
    }
}
