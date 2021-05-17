using HBSIS.ReservaMesas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HBSIS.ReservaMesas.Persistence.Mappings
{
    public class UnityMapConfig : IEntityTypeConfiguration<Unity>
    {
        public void Configure(EntityTypeBuilder<Unity> builder)
        {
            builder.ToTable("Units");

            builder.Property(x => x.Id)
                .UseIdentityColumn();

            builder.Property(x => x.Name)
                .HasMaxLength(35)
                .IsRequired();

            builder.Property(x => x.Active)
                .IsRequired();

            builder.Property(x => x.Deleted)
                .HasDefaultValue(false);
        }
    }
}
