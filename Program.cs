using System.Data;
using Microsoft.Data.SqlClient;
using TaskManagementMVC.Repository;
using TaskManagementMVC.Services;

namespace TaskManagementMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); 
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
             ?? throw new InvalidOperationException("Database connection string is missing in appsettings.json.");

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSingleton<IDbConnection>(sp => new SqlConnection(connectionString));

            //add UserRepository
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();
            builder.Services.AddScoped<ITaskServices, TaskServices>();

            builder.Services.AddHttpContextAccessor(); // help to manage session 
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
            app.UseSession();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}");

            app.Run();
        }
    }
}
