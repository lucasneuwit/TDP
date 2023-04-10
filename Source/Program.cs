using Microsoft.EntityFrameworkCore;
using TDP.Models.Domain;
using TDP.Models.Persistence;
using TDP.Models.Persistence.Repository.Factory;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<TdpDbContext>(opts => opts.UseSqlServer(connectionString));
builder.Services.AddTransient(typeof(IRepository<>),sp =>
{
    var factory = new RepositoryFactory();
    factory.GetRepository<>()
})


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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