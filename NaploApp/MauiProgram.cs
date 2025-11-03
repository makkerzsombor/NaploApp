using Microsoft.Maui;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using NaploApp.Data;
using System.IO;
using NaploApp.ViewModels;
using NaploApp.Pages;
using NaploApp.Models;

namespace NaploApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkitCamera() // keep if you use camera toolkit
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // runtime DB path (file on device/emulator)
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "naploapp.db");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite($"Data Source={dbPath}"));

            // register viewmodels/pages
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<HomePage>();

            var app = builder.Build();

            // Ensure DB and seed sample data (quick dev fix)
            try
            {
                using var scope = app.Services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                // Creates DB/tables from model if they don't exist (no migrations required)
                db.Database.EnsureCreated();

                // optional: seed one post so you can see items immediately
                if (!db.Posts.Any())
                {
                    db.Posts.Add(new Post { Description = "Első bejegyzés (seed)", Date = DateTime.Now });
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // you can log the exception for troubleshooting
                System.Diagnostics.Debug.WriteLine($"DB ensure/seed failed: {ex}");
            }

            return app;
        }
    }
}
