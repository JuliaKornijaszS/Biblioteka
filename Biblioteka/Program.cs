using Biblioteka.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<BibliotekaContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<BibliotekaContext>();

// MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Ksiazki}/{action=Index}/{id?}");

// Seed danych (autorzy, kategorie, role)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<BibliotekaContext>();

    context.Database.EnsureCreated();

    if (!context.Autorzy.Any())
    {
        context.Autorzy.AddRange(
            new Biblioteka.Models.Entities.Autor { ImieNazwisko = "Adam Mickiewicz" },
            new Biblioteka.Models.Entities.Autor { ImieNazwisko = "Henryk Sienkiewicz" }
        );
    }

    if (!context.Kategorie.Any())
    {
        context.Kategorie.AddRange(
            new Biblioteka.Models.Entities.Kategoria { Nazwa = "Powieœæ" },
            new Biblioteka.Models.Entities.Kategoria { Nazwa = "Fantastyka" }
        );
    }

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string[] roles = { "Admin", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    var adminEmail = "admin@biblioteka.pl";
    var admin = await userManager.FindByEmailAsync(adminEmail);

    if (admin == null)
    {
        var user = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail
        };

        await userManager.CreateAsync(user, "Admin123!");
        await userManager.AddToRoleAsync(user, "Admin");
    }

    context.SaveChanges();
}

app.Run();
