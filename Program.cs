using Microsoft.EntityFrameworkCore;
using REF_XML_REQUEST.Models;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(connection) + "is Null");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Host.UseWindowsService();
builder.Services.AddWindowsService();

builder.WebHost.ConfigureKestrel((context, options) =>
{
    options.Listen(IPAddress.Any, 8091); //Http
    options.Listen(IPAddress.Any, 8090, ListenOptions =>
    {
        ListenOptions.UseHttps(); //Https   (можно в ковычках указать пусть до своего серта с паролем)
    });
});


var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
