using HBSIS.ReservaMesas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HBSIS.ReservaMesas.Persistence.Mappings
{
    public class WorkstationMapConfig : IEntityTypeConfiguration<Workstation>
    {
        public void Configure(EntityTypeBuilder<Workstation> builder)
        {
            builder.ToTable("Workstations");

            builder.Property(x => x.Id)
               .UseIdentityColumn();

            builder.Property(x => x.Name)
                .HasMaxLength(35)
                .IsRequired();

            builder.HasIndex(x => x.Name);

            builder.Property(x => x.Active)
                .IsRequired();

            builder.HasOne(x => x.Floor)
                .WithMany(x => x.Workstations)
                .HasForeignKey(x => x.FloorId);
        }
    }
}
