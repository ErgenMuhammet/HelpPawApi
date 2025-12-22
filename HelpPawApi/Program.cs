using HelpPaw.Infrustructure;
using HelpPaw.Persistence;
using HelpPawApi.Application;
using HelpPawApi.Domain.Entities.AppRole;
using HelpPawApi.Domain.Entities.AppUser;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using HelpPawApi.Application.Interfaces; // IAppContext için
using HelpPaw.Persistence.Context;       // AdvertisementsContext için

var builder = WebApplication.CreateBuilder(args);

// --- 1. SERVICES KISMI ---

// Swagger Ayarlarý
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "HelpPaw API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Token'ý yapýþtýrmanýz yeterlidir (Bearer yazmanýza gerek yok)",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Katmanlarýn Servis Kayýtlarý
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<IAppContext, IdentityContext>(); //

// JWT Ayarlarý
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// --- 2. MIDDLEWARE & SEED DATA KISMI ---

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Gerekli servisleri çaðýrýyoruz
        var userManager = services.GetRequiredService<UserManager<AppUsers>>();
        var roleManager = services.GetRequiredService<RoleManager<AppRole>>();

        // EKLENDÝ: Ýlan eklemek için Context'i de çaðýrmamýz lazým
        var context = services.GetRequiredService<IdentityContext>();

        // 1. ADIM: Önce Kullanýcýlarý ve Rolleri Oluþtur
        var userSeedData = new HelpPaw.Persistence.Context.UserSeedData(userManager, roleManager);
        await userSeedData.InitializeAsync();

        // 2. ADIM: Sonra Ýlanlarý Oluþtur (Çünkü ilanlar kullanýcýlara baðlý)
        var adSeedData = new HelpPaw.Persistence.Context.AdvertisementsContext(userManager, context);
        await adSeedData.InitializeAsync();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Seed Data yüklenirken bir hata oluþtu!");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication(); // 1. Kimlik Doðrulama
app.UseAuthorization();  // 2. Yetkilendirme

app.MapControllers();

app.Run();