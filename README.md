Teknik Mimari ve Tasarım Desenleri

Proje, sürdürülebilir ve ölçeklenebilir bir yapı sunmak amacıyla modern yazılım mimarileri üzerine inşa edilmiştir:

Onion Architecture (Soğan Mimarisi): İş mantığı merkeze alınarak, dış bağımlılıkların (DB, UI, API) iç katmanlardan izole edilmesi sağlandı.

CQRS & MediatR: Okuma ve yazma operasyonları birbirinden ayrılarak uygulama performansı optimize edildi ve katmanlar arası bağımlılık (tight coupling) minimize edildi.

Custom Identity & Security: Hazır Identity kütüphaneleri yerine, güvenlik mimarisi (Kullanıcı yönetimi, Rol tabanlı yetkilendirme ve Password Hashing) projeye özgü olarak manuel kurgulandı.

Kullanılan Teknolojiler

Framework: .NET 8 / ASP.NET Core

ORM: Entity Framework Core

Veritabanı: PostgreSQL / MS SQL Server

Güvenlik: Custom JWT (JSON Web Token) Implementation, BCrypt/SHA256 Password Hashing

İletişim: SMTP tabanlı güvenli e-posta doğrulama hizmeti

Araçlar: AutoMapper, FluentValidation

Öne Çıkan Özellikler

Acil Durum Bildirimi: Kullanıcılar yaralı bir hayvan bulduğunda konum ve durum bilgisiyle hızlıca ihbar oluşturabilir.

Veteriner Koordinasyonu: Bölgedeki gönüllü veterinerler sisteme dahil olabilir ve vakaları üstlenebilir.

Özel Kimlik Doğrulama: Manuel olarak geliştirilmiş, JWT tabanlı ve claims-oriented güvenlik altyapısı.

E-posta Doğrulama: Kayıt sırasında güvenliği artırmak amacıyla SMTP entegrasyonlu doğrulama adımı.

Gelişmiş Veri Yönetimi: EF Core ile optimize edilmiş ilişkisel veritabanı şeması ve TPT/TPH stratejileri.

Proje Yapısı

Domain: Temel Entity'ler, Global Değişmezler ve Core nesneler.

Application: İş Mantığı, CQRS (Commands/Queries), DTO'lar ve Mapping süreçleri.

Persistence: Veritabanı Context yapısı, Migration yönetimi ve Repository implementasyonları.

Infrastructure: JWT üretimi, SMTP servisi ve şifreleme algoritmaları gibi harici servisler.

Presentation: RESTful Web API Controller'ları.
