using Microsoft.EntityFrameworkCore;
using CustomMonopoly.Server.Data;
using CustomMonopoly.Server.Models;
using Microsoft.AspNetCore.Identity;
using CustomMonopoly.Server.Services;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(
        options => options.UseSqlServer("name=ConnectionStrings:Database")
);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddApiEndpoints();

// DI Services
builder.Services.AddScoped<GameService>();
builder.Services.AddScoped<GameEventHandlingService>();
builder.Services.AddScoped<DatabaseInitializer>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapGet("/pingauth", (ClaimsPrincipal user) =>
{
    var email = user.FindFirstValue(ClaimTypes.Email);
    if (email == null)
    {
        return Results.Unauthorized();
    }
    return Results.Json(new { Email = email });
    // if user is signed in return success else return 404 not authorized
}).RequireAuthorization();

app.MapPost("/signout", async (SignInManager<ApplicationUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.Ok();
});

app.MapControllers();
app.MapIdentityApi<ApplicationUser>();
app.MapFallbackToFile("/index.html");

// Initialize the database
using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
    initializer.Initialize();
}


app.Run();
