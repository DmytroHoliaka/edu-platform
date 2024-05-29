using EduPlatform.Domain.Commands;
using EduPlatform.Domain.Queries;
using EduPlatform.EntityFramework.Commands;
using EduPlatform.EntityFramework.DbContextConfigurations;
using EduPlatform.EntityFramework.Queries;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuestPDF.Infrastructure;

namespace EduPlatform.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices(
                    (context, services) =>
                    {
                        // ToDo: Use JSON configuration file #1
                        const string connectionString =
                            @"Server=(localdb)\mssqllocaldb;Database=EduPlatform2;Trusted_Connection=True;";

                        services.AddSingleton<DbContextOptions>(
                            new DbContextOptionsBuilder().UseSqlServer(connectionString).Options);
                        services.AddSingleton<EduPlatformDbContextFactory>();

                        services.AddSingleton<IGetAllCoursesQuery, GetAllCoursesQuery>();
                        services.AddSingleton<ICreateCourseCommand, CreateCourseCommand>();
                        services.AddSingleton<IUpdateCourseCommand, UpdateCourseCommand>();
                        services.AddSingleton<IDeleteCourseCommand, DeleteCourseCommand>();

                        services.AddSingleton<IGetAllGroupsQuery, GetAllGroupsQuery>();
                        services.AddSingleton<ICreateGroupCommand, CreateGroupCommand>();
                        services.AddSingleton<IUpdateGroupCommand, UpdateGroupCommand>();
                        services.AddSingleton<IDeleteGroupCommand, DeleteGroupCommand>();

                        services.AddSingleton<IGetAllStudentsQuery, GetAllStudentsQuery>();
                        services.AddSingleton<ICreateStudentCommand, CreateStudentCommand>();
                        services.AddSingleton<IUpdateStudentCommand, UpdateStudentCommand>();
                        services.AddSingleton<IDeleteStudentCommand, DeleteStudentCommand>();

                        services.AddSingleton<IGetAllTeachersQuery, GetAllTeachersQuery>();
                        services.AddSingleton<ICreateTeacherCommand, CreateTeacherCommand>();
                        services.AddSingleton<IUpdateTeacherCommand, UpdateTeacherCommand>();
                        services.AddSingleton<IDeleteTeacherCommand, DeleteTeacherCommand>();

                        services.AddSingleton<ModalNavigationStore>();
                        services.AddSingleton<ViewStore>();

                        services.AddSingleton<CourseStore>();
                        services.AddSingleton<GroupStore>();
                        services.AddSingleton<StudentStore>();
                        services.AddSingleton<TeacherStore>();
                        services.AddSingleton<HubViewModel>();

                        services.AddSingleton<MainViewModel>();
                        services.AddSingleton<MainWindow>(serviceProvider => new MainWindow()                        
                        {
                            DataContext = serviceProvider.GetRequiredService<MainViewModel>()
                        });
                    })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            EduPlatformDbContextFactory contextFactory = _host.Services.GetRequiredService<EduPlatformDbContextFactory>();
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