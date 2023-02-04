using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using NicasourceChallenge.Core.Entities;
using NicasourceChallenge.Core.Interfaces;
using NicasourceChallenge.Core.Services;
using NicasourceChallenge.Infrastructure.Data;
using NicasourceChallenge.Infrastructure.Repositories;
using NicasourceChallenge.SharedKernel.Interfaces;
using NicasourceChallenge.Web._keenthemes;
using NicasourceChallenge.Web._keenthemes.libs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var cosmosConnectionString = builder.Configuration["CosmosConnectionString:ConnectionString"];
var databaseName = builder.Configuration["CosmosConnectionString:DatabaseName"];

builder.Services.AddDbContext<CosmosDbContext>(options => options.UseCosmos(cosmosConnectionString!, databaseName!));

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"));

builder.Services.AddTransient<IDocumentService, DocumentService>();
builder.Services.AddTransient<IAzureStorageRepository<Blob, BlobResponse>, AzureStorageRepository>();

builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IKTTheme, KTTheme>();
builder.Services.AddSingleton<IKTBootstrapBase, KTBootstrapBase>();

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("themesettings.json")
    .Build();

KTThemeSettings.init(configuration);

var app = builder.Build();

app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Request.Path = "/not-found";
        await next();
    }
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/System/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseThemeMiddleware();

app.MapControllerRoute(
    "default",
    "{controller=Cloud}/{action=Index}/{id?}");

app.Run();