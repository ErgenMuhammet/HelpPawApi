using HelpPawApi.Domain.Entities.AppUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace HelpPaw.Persistence.Configuration
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUsers>
    {
        public void Configure(EntityTypeBuilder<AppUsers> builder)
        {
            builder.OwnsOne(u => u.Location, loc =>
            {
                loc.Property(l => l.latitude).HasColumnName("Latitude");
                loc.Property(l => l.longitude).HasColumnName("Longitude");
            });

            // --- [YENİ EKLENEN KISIM] ---

            // Tablo İsimlendirmeleri
            builder.ToTable("AspNetUsers");
            
        }
    }
    
}
