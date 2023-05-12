using AutoMapper;
using TDP.Extensions;
using TDP.Models.Application.DataTransfer.MappingProfiles;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
var url = builder.Configuration.GetValue<string>("ApiUrl");

// Add services to the container.
builder.Services.AddOmdbProvider();
builder.Services.AddOmdbHttpClient(url!);
builder.Services.AddMovieService();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext(connectionString!);
builder.Services.AddRepository();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Configure swagger.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();