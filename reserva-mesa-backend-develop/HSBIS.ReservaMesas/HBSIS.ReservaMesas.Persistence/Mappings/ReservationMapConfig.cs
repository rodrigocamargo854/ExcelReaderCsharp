using HBSIS.ReservaMesas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HBSIS.ReservaMesas.Persistence.Mappings
{
    public class ReservationMapConfig : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservation");

            builder.Property(x => x.Id)
                .UseIdentityColumn();

            builder.Property(x => x.Date)
                .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.HasOne(x => x.Workstation)
                .WithMany(x => x.Reservations)
                .HasForeignKey(x => x.WorkstationId)
                .IsRequired();

            builder.HasIndex(x => new { x.WorkstationId, x.Date })
                .IsUnique();

            builder.Property(x => x.CanceledOn)
                .HasDefaultValue();
        }
    }
}
