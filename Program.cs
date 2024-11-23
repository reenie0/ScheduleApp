using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ScheduleApp.Data;
using ScheduleApp.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        // Add the ApplicationDbContext and configure it to use SQL Server
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Add Identity services with ApplicationUser (Users) and IdentityRole
        builder.Services.AddIdentity<Users, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = true; // Ensure email is unique
        })

        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

        // Configure cookie authentication
        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Account/Login"; // Redirect to Login path if not authenticated
            options.LogoutPath = "/Account/Logout"; // Redirect to Logout path after sign-out
            options.AccessDeniedPath = "/Account/AccessDenied"; // Redirect when access is denied
            options.SlidingExpiration = true; // Enable sliding expiration
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Set cookie expiration time
        });

        // Add policies for authorization
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("StudentOnly", policy =>
                policy.RequireClaim("UserType", "Student"));
            options.AddPolicy("LecturerOnly", policy =>
                policy.RequireClaim("UserType", "Lecturer"));
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication(); // Ensure authentication middleware is used
        app.UseAuthorization(); // Ensure authorization middleware is used

        // Define the default route.
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}"); // Change this to point to Home or another controller

        // Run the application
        app.Run();
    }
}

