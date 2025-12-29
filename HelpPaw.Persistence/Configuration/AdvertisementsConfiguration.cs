    using HelpPawApi.Domain.Entities.Advertisement;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class AdvertisementsConfiguration : IEntityTypeConfiguration<Advertisements>
    {
        public void Configure(EntityTypeBuilder<Advertisements> builder)
        {
            builder.OwnsOne(a => a.Location, loc =>
            {
                loc.Property(l => l.latitude).HasColumnName("Location_Latitude");
                loc.Property(l => l.longitude).HasColumnName("Location_Longitude");
            });

            
            builder.HasOne(a => a.User)
                .WithMany(u => u.CreatedAdvertisements)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Vet)
                .WithMany(v => v.TreatedAdvertisements)
                .HasForeignKey(a => a.VetId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }