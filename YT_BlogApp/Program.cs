
using Blog.Administrator.Repository;
using Blog_DatabaseAccess.SqlDataAccess;
using Blog.Auth.Repository;
using Blog.Author.Repository;
using YT_BlogApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(option => option.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddTransient<IAdministratorRepository, AdministratorRepository>();
builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();
builder.Services.AddTransient<IUserInfoServices, UserInfoServices>();

builder.Services.AddTransient<IAuthRepository, AuthRepository>();//Authentication

/*----Hashing passwords------*/
builder.Services.AddTransient<PasswordMigrationService>(); // Register PasswordMigrationService
builder.Services.AddLogging(); // Add logging for migration
/*--------*/

// Register IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

////Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Home/SignIn";
            options.AccessDeniedPath = "/Home/SignIn";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(10); // Optional: Set cookie expiration
        });
///-----

var app = builder.Build();

/*-----Hash keys for passwords*-------*/
// Run the password migration (only in development or once)
if (app.Environment.IsDevelopment()) // Optional: Run only in development
{
    using (var scope = app.Services.CreateScope())
    {
        var migrationService = scope.ServiceProvider.GetService<PasswordMigrationService>();
        await migrationService.HashExistingPasswords();
    }
}

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
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
