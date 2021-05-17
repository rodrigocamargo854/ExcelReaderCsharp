using HBSIS.ReservaMesas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HBSIS.ReservaMesas.Persistence.Mappings
{
    public class FloorMapConfig : IEntityTypeConfiguration<Floor>
    {
        public void Configure(EntityTypeBuilder<Floor> builder)
        {
            builder.ToTable("Floors");

            builder.Property(x => x.Id)
                .UseIdentityColumn();

            builder.Property(x => x.Name)
                .HasMaxLength(35)
                .IsRequired();

            builder.Property(x => x.Active)
                .IsRequired();

            builder.Property(x => x.Code)
                .IsRequired();

            builder.HasOne(x => x.Unity)
                .WithMany(x => x.Floors)
                .HasForeignKey(x => x.UnityId);
        }
    }
}
