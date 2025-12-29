using HelpPawApi.Domain.Entities.AppRole;
using HelpPawApi.Domain.Entities.AppUser;
using HelpPawApi.Domain.Entities.Locations; // Location nesnesi için gerekli
using Microsoft.AspNetCore.Identity;

namespace HelpPaw.Persistence.Context
{
    public class UserSeedData
    {
        private readonly UserManager<AppUsers> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UserSeedData(UserManager<AppUsers> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitializeAsync()
        {
            
            string[] roleNames = { "Veteriner", "Kullanıcı" };
            foreach (var roleName in roleNames)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new AppRole { Name = roleName });
                }
            }

           
            if (_userManager.Users.Any()) return;


            var veterinerler = new List<Vet>
            {
                new Vet {
                   
                    UserName = "ahmet.vet@klinik.com",
                    Email = "ahmet.vet@klinik.com",
                    EmailConfirmed = true,
                
                    PhoneNumber = "0216 345 88 90", 
                    
                   
                    FullName = "Ahmet Yılmaz",
                    City = "İstanbul",
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                   
                    Location = new Location(40.9632, 29.0654), 

                 
                    VeterinaryClinicName = "Bağdat Cad. Veteriner Kliniği",
                    VeterinerRegistiryNumber = "VET-3401"
                },

                new Vet {
                    UserName = "selin.kaya@patidostu.com",
                    Email = "selin.kaya@patidostu.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0312 444 55 66", 

                    FullName = "Selin Kaya",
                    City = "Ankara",
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
              
                    Location = new Location(39.9208, 32.8541),

                    VeterinaryClinicName = "Pati Dostu Kliniği",
                    VeterinerRegistiryNumber = "VET-0601"
                },

                new Vet {
                    UserName = "mehmet.vet@bursavet.com",
                    Email = "mehmet.vet@bursavet.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0224 233 11 22",

                    FullName = "Mehmet Demir",
                    City = "Bursa",
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    Location = new Location(40.2000, 29.0700),

                    VeterinaryClinicName = "Bursa Veteriner Kliniği",
                    VeterinerRegistiryNumber = "VET-1601"
                },

                new Vet {
                    UserName = "ayse.celik@izmirvet.com",
                    Email = "ayse.celik@izmirvet.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0232 464 35 35",

                    FullName = "Ayşe Çelik",
                    City = "İzmir",
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    Location = new Location(38.4300, 27.1500),

                    VeterinaryClinicName = "Alsancak İzmir Veteriner",
                    VeterinerRegistiryNumber = "VET-3501"
                },

                new Vet {
                    UserName = "can.vet@petsaglik.com",
                    Email = "can.vet@petsaglik.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0242 323 90 90",

                    FullName = "Can Öztürk",
                    City = "Antalya",
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    Location = new Location(36.9000, 30.7200),

                    VeterinaryClinicName = "Pet Sağlık Kliniği",
                    VeterinerRegistiryNumber = "VET-0701"
                }
            };

            foreach (var vet in veterinerler)
            {
        
                var result = await _userManager.CreateAsync(vet, "123456Aa.");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(vet, "Veteriner");
                }
            }


        
            var kullanicilar = new List<User>
            {
                new User {
                    UserName = "cem.yilmaz@gmail.com",
                    Email = "cem.yilmaz@gmail.com",
                    FullName = "Cem Yılmaz",
                    PhoneNumber = "0532 100 00 01",
                    City = "İstanbul",
                    EmailConfirmed = true,
                    CreatedDate = DateTime.UtcNow,
                   
                    Location = new Location(41.0082, 28.9784)
                },
                new User {
                    UserName = "fatma.kara@hotmail.com",
                    Email = "fatma.kara@hotmail.com",
                    FullName = "Fatma Kara",
                    PhoneNumber = "0533 200 00 02",
                    City = "Ankara",
                    EmailConfirmed = true,
                    CreatedDate = DateTime.UtcNow,
                    Location = new Location(39.9208, 32.8541)
                },
                new User {
                    UserName = "hakan.uslu@outlook.com",
                    Email = "hakan.uslu@outlook.com",
                    FullName = "Hakan Uslu",
                    PhoneNumber = "0535 300 00 03",
                    City = "İzmir",
                    EmailConfirmed = true,
                    CreatedDate = DateTime.UtcNow,
                    Location = new Location(38.4192, 27.1287)
                },
                new User {
                    UserName = "pinar.gunes@yahoo.com",
                    Email = "pinar.gunes@yahoo.com",
                    FullName = "Pınar Güneş",
                    PhoneNumber = "0542 400 00 04",
                    City = "Bursa",
                    EmailConfirmed = true,
                    CreatedDate = DateTime.UtcNow,
                    Location = new Location(40.1828, 29.0667)
                },
                new User {
                    UserName = "emre.aydin@gmail.com",
                    Email = "emre.aydin@gmail.com",
                    FullName = "Emre Aydın",
                    PhoneNumber = "0555 500 00 05",
                    City = "Antalya",
                    EmailConfirmed = true,
                    CreatedDate = DateTime.UtcNow,
                    Location = new Location(36.8841, 30.7056)
                }
            };

            foreach (var user in kullanicilar)
            {
                var result = await _userManager.CreateAsync(user, "123456Aa.");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Kullanıcı");
                }
            }
        }
    }
}