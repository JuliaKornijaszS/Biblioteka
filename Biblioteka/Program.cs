using Biblioteka.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BibliotekaContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Ksiazki}/{action=Index}/{id?}")
    .WithStaticAssets();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<Biblioteka.Models.BibliotekaContext>();

    // Upewniamy siê, ¿e baza istnieje
    context.Database.EnsureCreated();

    // Autorzy
    if (!context.Autorzy.Any())
    {
        context.Autorzy.Add(new Biblioteka.Models.Entities.Autor
        {
            ImieNazwisko = "Adam Mickiewicz"
        });

        context.Autorzy.Add(new Biblioteka.Models.Entities.Autor
        {
            ImieNazwisko = "Henryk Sienkiewicz"
        });
    }

    // Kategorie
    if (!context.Kategorie.Any())
    {
        context.Kategorie.Add(new Biblioteka.Models.Entities.Kategoria
        {
            Nazwa = "Powieœæ"
        });

        context.Kategorie.Add(new Biblioteka.Models.Entities.Kategoria
        {
            Nazwa = "Fantastyka"
        });
    }

    context.SaveChanges();
}


app.Run();
