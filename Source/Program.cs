using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TDP.Models.Domain;
using TDP.Models.Application;
using TDP.Models.Application.DataTransfer.MappingProfiles;
using TDP.Models.Application.Services;
using TDP.Models.Domain;
using TDP.Models.Persistence;
using TDP.Models.Persistence.Repository;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
var url = builder.Configuration.GetValue<string>("ApiUrl");
builder.Services.AddHttpClient("OMDBApi",httpClient => { 
    httpClient.BaseAddress = new Uri(url); });
builder.Services.AddTransient<IMovieService, MovieService>();
builder.Services.AddTransient<IApiProvider, OmdbProvider>();
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<TdpDbContext>(opts => opts.UseSqlServer(connectionString));
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddSwaggerGen();



var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MovieMappingProfile());
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
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