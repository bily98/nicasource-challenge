using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using NicasourceChallenge.Web.Configurations.AuthorizationPolicies;
using NicasourceChallenge.Web._keenthemes;
using NicasourceChallenge.Web._keenthemes.libs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminScope",
        policy => policy.Requirements.Add(
            new ScopesRequirement(
                "https://bily98.onmicrosoft.com/cycloware/api/admin"))
    );
});

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
    app.UseExceptionHandler("/Home/Error");
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
    name: "default",
    pattern: "{controller=Dashboards}/{action=Index}/{id?}");

app.Run();
