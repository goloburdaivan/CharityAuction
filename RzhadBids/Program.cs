using Microsoft.EntityFrameworkCore;
using RzhadBids.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DatabaseContext>(options => options.UseMySql(connection,
    ServerVersion.AutoDetect(connection)));
builder.Services.AddScoped<IPhotoUploadService, PhotoStorageService>();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
