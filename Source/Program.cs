using AutoMapper;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using TDP.Extensions;
using TDP.Models.Application.DataTransfer.MappingProfiles;
using TDP.Models.Application.Middleware;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
var url = builder.Configuration.GetValue<string>("ApiUrl");

// Add services to the container.
builder.Services.AddOmdbProvider();
builder.Services.AddOmdbHttpClient(url!);
builder.Services.AddMovieService();
builder.Services.AddUserService();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext(connectionString!);
builder.Services.AddUnitOfWork();
builder.Services.AddRepository();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper();
builder.Services.AddSerilog((services, config) =>
{
    config.WriteTo.Console(
        LogEventLevel.Information,
        theme: AnsiConsoleTheme.Code);
    config.WriteTo.File(
        "log.txt",
        LogEventLevel.Debug,
        rollingInterval: RollingInterval.Day);
});

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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

app.UseStaticFiles();
app.UseRouting();
app.UseMiddleware<UnitOfWorkMiddleware>();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();