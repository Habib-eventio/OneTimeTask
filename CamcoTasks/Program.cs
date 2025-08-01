using Append.Blazor.Printing;
using BlazorDownloadFile;
using Blazored.Toast;
using CamcoTasks.AppSettings;
using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Common.Email;
using CamcoTasks.Infrastructure.Entities;
using CamcoTasks.Infrastructure.Entities.Login;
using CamcoTasks.Service.IService;
using CamcoTasks.Service.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor;
using System;
using System.Collections.Generic;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

string env = builder.Environment.EnvironmentName;
builder.Configuration
       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
       .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true);

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<DatabaseContext>(opts =>
    opts.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sql => sql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
    ),
    ServiceLifetime.Transient
);

builder.Services
    .AddIdentity<User, Role>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;

        options.Password.RequireDigit = false;
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;
    })
    .AddEntityFrameworkStores<DatabaseContext>()
    .AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(opts =>
{
    opts.LoginPath = "/Account/Login";
    opts.ExpireTimeSpan = TimeSpan.FromDays(14);
    opts.SlidingExpiration = true;
    opts.Cookie.HttpOnly = true;
    opts.Cookie.SameSite = SameSiteMode.Lax;
});
builder.Services.AddControllers()
       .AddNewtonsoftJson(o =>
           o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
       );
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor()
       .AddCircuitOptions(o => o.DetailedErrors = true);

builder.Services.AddSyncfusionBlazor();
builder.Services.AddBlazoredToast();
builder.Services.AddBlazorDownloadFile(ServiceLifetime.Scoped);
builder.Services.AddScoped<IPrintingService, PrintingService>();


builder.Services.AddLocalization(opts => opts.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(opts =>
{
    var supported = new List<CultureInfo> { new("en-US") };
    opts.DefaultRequestCulture = new RequestCulture("en-US");
    opts.SupportedCultures = supported;
    opts.SupportedUICultures = supported;
    opts.RequestCultureProviders = new[] { new QueryStringRequestCultureProvider() };
});

CamcoTasks.Version.Number = builder.Configuration["Version"];

builder.Services.AppService();
builder.Services.AppRepository();

var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddScoped<IUserContextService, UserContextService>();

var app = builder.Build();

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NzE3NDUyQDMyMzAyZTMyMmUzMFNuelhLb0dOVlgxMSsxMUdZMTJlaWdKTjJFV3ZvV2FvRHpGdmR6MjZ6Mms9");

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRequestLocalization();
app.UseWebSockets();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    var path = context.Request.Path;

    var isAllowedPath =
        path.StartsWithSegments("/Account/Login", StringComparison.OrdinalIgnoreCase) ||
        path.StartsWithSegments("/login", StringComparison.OrdinalIgnoreCase) ||
        path.StartsWithSegments("/account", StringComparison.OrdinalIgnoreCase) ||
        path.StartsWithSegments("/_blazor", StringComparison.OrdinalIgnoreCase) ||
        path.StartsWithSegments("/_framework", StringComparison.OrdinalIgnoreCase) ||
        path.StartsWithSegments("/css", StringComparison.OrdinalIgnoreCase) ||
        path.StartsWithSegments("/js", StringComparison.OrdinalIgnoreCase) ||
        path.StartsWithSegments("/lib", StringComparison.OrdinalIgnoreCase) ||
        path.Value.EndsWith(".ico", StringComparison.OrdinalIgnoreCase);

    if (!context.User.Identity.IsAuthenticated && !isAllowedPath)
    {
        context.Response.Redirect("/Account/Login");
        return;
    }

    await next();
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapBlazorHub();
    endpoints.MapFallbackToPage("/_Host");
});

app.Run();
