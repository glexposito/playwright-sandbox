using Microsoft.EntityFrameworkCore;
using PlaywrightSandbox.Web.Data;

var builder = WebApplication.CreateBuilder(args);

if (!builder.Environment.IsProduction())
    builder.WebHost.UseStaticWebAssets();

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Games")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

if (app.Environment.IsProduction())
    app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();


app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Games}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
