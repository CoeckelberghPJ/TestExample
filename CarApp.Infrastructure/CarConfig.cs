using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CarApp.Core.Domain;

namespace CarApp.Infrastructure
{
    internal class CarConfig : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.Brand).IsRequired();
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Color).IsRequired();

            builder.OwnsOne(x => x.VinNumber);
            builder.OwnsMany(x => x.FaultCodes, ownedBuilder =>
            {
            });
        }
    }
}
