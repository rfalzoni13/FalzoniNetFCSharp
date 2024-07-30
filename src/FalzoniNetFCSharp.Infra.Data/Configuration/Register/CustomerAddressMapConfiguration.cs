using FalzoniNetFCSharp.Domain.Entities.Register;
using System.Data.Entity.ModelConfiguration;

namespace FalzoniNetFCSharp.Infra.Data.Configuration.Register
{
    public class CustomerAddressMapConfiguration : EntityTypeConfiguration<CustomerAddress>
    {
        public CustomerAddressMapConfiguration()
        {
            HasKey(c => c.Id);

            Property(c => c.PostalCode).IsRequired().HasMaxLength(10);
            
            Property(c => c.AddressName).IsRequired().HasMaxLength(512);
            
            Property(c => c.Number).IsRequired();
            
            Property(c => c.Complement).IsOptional().HasMaxLength(256);
            
            Property(c => c.Neighborhood).IsRequired().HasMaxLength(256);
            
            Property(c => c.City).IsRequired().HasMaxLength(128);
            
            Property(c => c.State).IsRequired().HasMaxLength(2);

            Property(c => c.Created).IsRequired();

            Property(c => c.Modified).IsOptional();        
        }
    }
}
