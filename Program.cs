using PetShopProject.DAL;
using PetShopProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using PetShopProject.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRepository<Animal>, AnimalRepository>();
builder.Services.AddScoped<IRepository<Comment>, CommentRepository>();
builder.Services.AddScoped<IRepository<Category>, CategoryRepository>();
builder.Services.AddScoped<IPictureService, PictureService>();
builder.Services.AddDbContext<AnimalContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Animal")));
builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("User")));
builder.Services.AddIdentity<UserModel, IdentityRole>().AddEntityFrameworkStores<UserDbContext>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 4;
    options.Password.RequiredUniqueChars = 0;
});

var app = builder.Build();

app.UseRouting();
app.MapControllerRoute("default", "{controller=Home}/{action=Index}");

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<AnimalContext>();
    ctx.Database.EnsureDeleted();
    ctx.Database.EnsureCreated();
    var ctx2 = scope.ServiceProvider.GetRequiredService<UserDbContext>();
    ctx2.Database.EnsureDeleted();
    ctx2.Database.EnsureCreated();
}
app.Environment.EnvironmentName = "Production";

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();


app.Run();
