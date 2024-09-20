using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportStore.server.Data.Contexts;
using SportStore.server.Data.Infrastructure;
using SportStore.server.Data.Models;
using SportStore.server.Data.SeedData;
using SportStore.server.Helpers;
using SportStore.server.Installers;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();

builder.Services.AddDbContext<IdentityApplicationDbContext>(options =>
    options.UseSqlServer(
        configuration.GetConnectionString("IdentityConnectionString")
    )
);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        configuration.GetConnectionString("SportStoreConnectionString")
    )
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", o => 
        o.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod());
});


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthenticationConfiguration(configuration);
builder.Services.AddAuthorization();

// custom services
builder.Services.AddScoped<JwtHelper>();
builder.Services.AddDataServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowSpecificOrigin");

await IdentitySeedData.EnsurePopulatedAsync(app, configuration);
await SeedData.EnsurePopulatedAsync(app);

app.MapControllers();

app.Run();
