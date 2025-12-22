using HelpPawApi.Application.Interfaces;
using HelpPawApi.Domain.Entities.Advertisement;
using HelpPawApi.Domain.Entities.AppUser;
using HelpPawApi.Domain.Entities.Locations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPaw.Persistence.Context
{
    public class AdvertisementsContext
    {
        private readonly UserManager<AppUsers> _userManager;
        private readonly IdentityContext _context;

        public AdvertisementsContext(UserManager<AppUsers> userManager, IdentityContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        public async Task InitializeAsync()
        {
           
            if (await _context.Advertisements.AnyAsync()) return;

            
            var cemUser = await _userManager.FindByEmailAsync("cem.yilmaz@gmail.com");
            var fatmaUser = await _userManager.FindByEmailAsync("fatma.kara@hotmail.com");
            var ahmetVet = await _userManager.FindByEmailAsync("ahmet.vet@klinik.com");

            if (cemUser == null || fatmaUser == null || ahmetVet == null) return;

            
            var advertisements = new List<Advertisements>
                {
                     new Advertisements
                    {
                        Id = Guid.NewGuid(),
                        Title = "Şahinbey'de yaralı köpek",
                        Description = "tahmini 4 yaşlarında bacağında büyük bir kesik yara var",
                        // City ve District kaldırıldı
                        AddressDescription = "istanbul",

                        CreatedTime = DateTime.UtcNow,

                        IsActive = true, // Veteriner yok ama ilan aktif
                        ImageUrl = "https://placehold.co/600x400?text=Kayip+Kedi",

                        userName = fatmaUser.FullName,
                        UserId = fatmaUser.Id,
                        VetId = null,
                        Location = new Location(40.9820, 29.0250)
                    },
                    
                    new Advertisements
                    {
                        Id = Guid.NewGuid(),
                        Title = "Kadıköy'de Kayıp Tekir Kedi",
                        Description = "3 yaşındaki tekir kedim Pamuk dün akşam kayboldu. Tasması kırmızı.",
                        // City ve District kaldırıldı
                        AddressDescription = "İstanbul",

                        CreatedTime = DateTime.UtcNow,

                        IsActive = true, // Veteriner yok ama ilan aktif
                        ImageUrl = "https://placehold.co/600x400?text=Kayip+Kedi",

                        userName = cemUser.FullName,
                        UserId = cemUser.Id,
                        VetId = null,
                        Location = new Location(40.9820, 29.0250)
                    },

                    
                    new Advertisements
                    {
                        Id = Guid.NewGuid(),
                        Title = "Köpeğim Halsiz ve Yemek Yemiyor",
                        Description = "Golden cinsi köpeğim 2 gündür çok halsiz, acil destek lazım.",
                        AddressDescription = "Kızılay meydanına yakın",

                        CreatedTime = DateTime.UtcNow.AddDays(-2),

                        
                        IsActive = false,
                        ImageUrl = "https://placehold.co/600x400?text=Hasta+Kopek",

                        userName = fatmaUser.FullName,
                        UserId = fatmaUser.Id,
                        VetId = ahmetVet.Id,
                        Location = new Location(39.9208, 32.8541)
                    },

                    new Advertisements
                    {
                        Id = Guid.NewGuid(),
                        Title = "Yavru Kedi Karma Aşı",
                        Description = "Sokakta bulduğum yavru kedi için aşı desteği arıyorum.",
                        AddressDescription = "Kız kulesi karşısı",

                        CreatedTime = DateTime.UtcNow.AddDays(-10),
                        ImageUrl = "https://placehold.co/600x400?text=Kayip+Kedi",

                       
                        IsActive = false,

                        userName = cemUser.FullName,
                        UserId = cemUser.Id,

                        VetId = ahmetVet.Id,
                        Location = new Location(41.0264, 29.0037)
                    }
                };

            // 4. Kaydet
            await _context.Advertisements.AddRangeAsync(advertisements);
            await _context.SaveChangesAsync();
        }
    }
}
