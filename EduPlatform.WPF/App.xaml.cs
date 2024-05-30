using EduPlatform.EntityFramework.DbContextConfigurations;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using EduPlatform.WPF.HostBuilders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuestPDF.Infrastructure;

namespace EduPlatform.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private readonly IHost _host =
            Host.CreateDefaultBuilder()
                .AddDbContext()
                .AddQueries()
                .AddCommands()
                .AddStores()
                .AddViewModels()
                .AddViews()
                .Build();

        protected override async void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            EduPlatformDbContextFactory contextFactory =
                _host.Services.GetRequiredService<EduPlatformDbContextFactory>();
            MainWindow mainWindow = _host.Services.GetRequiredService<MainWindow>();
            HubViewModel hubVM = _host.Services.GetRequiredService<HubViewModel>();

            using (EduPlatformDbContext context = contextFactory.Create())
            {
                await context.Database.MigrateAsync();
            }

            await hubVM.ConfigureDataFromDatabase();

            mainWindow.Show();
            base.OnStartup(e);

            QuestPDF.Settings.License = LicenseType.Community;
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }
    }
}