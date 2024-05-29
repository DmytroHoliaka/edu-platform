using EduPlatform.Domain.Commands;
using EduPlatform.Domain.Queries;
using EduPlatform.EntityFramework.Commands;
using EduPlatform.EntityFramework.DbContextConfigurations;
using EduPlatform.EntityFramework.Queries;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using QuestPDF.Infrastructure;

namespace EduPlatform.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly EduPlatformDbContextFactory _eduPlatformDbContextFactory;

        private IGetAllCoursesQuery _getAllCoursesQuery;
        private ICreateCourseCommand _createCourseCommand;
        private IUpdateCourseCommand _updateCourseCommand;
        private IDeleteCourseCommand _deleteCourseCommand;

        private IGetAllGroupsQuery _getAllGroupsQuery;
        private ICreateGroupCommand _createGroupCommand;
        private IUpdateGroupCommand _updateGroupCommand;
        private IDeleteGroupCommand _deleteGroupCommand;
        
        private IGetAllStudentsQuery _getAllStudentsQuery;
        private ICreateStudentCommand _createStudentCommand;
        private IUpdateStudentCommand _updateStudentCommand;
        private IDeleteStudentCommand _deleteStudentCommand;

        private IGetAllTeachersQuery _getAllTeachersQuery;
        private ICreateTeacherCommand _createTeacherCommand;
        private IUpdateTeacherCommand _updateTeacherCommand;
        private IDeleteTeacherCommand _deleteTeacherCommand;

        private ModalNavigationStore _modalNavigationStore;
        private CourseStore _courseStore;
        private GroupStore _groupStore;
        private StudentStore _studentStore;
        private TeacherStore _teacherStore;
        private ViewStore _viewStore;
        private HubViewModel _eduPlatformViewModel;
        
        public App()
        {
            // ToDo: Use JSON configuration file #1
            string connectionString = @"Server=(localdb)\mssqllocaldb;Database=EduPlatform;Trusted_Connection=True;";

            DbContextOptions options = new DbContextOptionsBuilder().UseSqlServer(connectionString).Options;
            _eduPlatformDbContextFactory = new EduPlatformDbContextFactory(options);

            InitializeServices();

        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            using (EduPlatformDbContext context = _eduPlatformDbContextFactory.Create())
            {
                await context.Database.MigrateAsync();
            }

            await _eduPlatformViewModel.ConfigureDataFromDatabase();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel
                (
                    _eduPlatformViewModel,
                    _modalNavigationStore
                )
            };

            MainWindow.Show();
            base.OnStartup(e);

            QuestPDF.Settings.License = LicenseType.Community;
        }

        private void InitializeServices()
        {
            _getAllCoursesQuery = new GetAllCoursesQuery(_eduPlatformDbContextFactory);
            _createCourseCommand = new CreateCourseCommand(_eduPlatformDbContextFactory);
            _updateCourseCommand = new UpdateCourseCommand(_eduPlatformDbContextFactory);
            _deleteCourseCommand = new DeleteCourseCommand(_eduPlatformDbContextFactory);

            _getAllGroupsQuery = new GetAllGroupsQuery(_eduPlatformDbContextFactory);
            _createGroupCommand = new CreateGroupCommand(_eduPlatformDbContextFactory);
            _updateGroupCommand = new UpdateGroupCommand(_eduPlatformDbContextFactory);
            _deleteGroupCommand = new DeleteGroupCommand(_eduPlatformDbContextFactory);

            _getAllStudentsQuery = new GetAllStudentsQuery(_eduPlatformDbContextFactory);
            _createStudentCommand = new CreateStudentCommand(_eduPlatformDbContextFactory);
            _updateStudentCommand = new UpdateStudentCommand(_eduPlatformDbContextFactory);
            _deleteStudentCommand = new DeleteStudentCommand(_eduPlatformDbContextFactory);

            _getAllTeachersQuery = new GetAllTeachersQuery(_eduPlatformDbContextFactory);
            _createTeacherCommand = new CreateTeacherCommand(_eduPlatformDbContextFactory);
            _updateTeacherCommand = new UpdateTeacherCommand(_eduPlatformDbContextFactory);
            _deleteTeacherCommand = new DeleteTeacherCommand(_eduPlatformDbContextFactory);

            _courseStore = new CourseStore(_getAllCoursesQuery,
                                           _createCourseCommand,
                                           _updateCourseCommand,
                                           _deleteCourseCommand);

            _groupStore = new GroupStore(_getAllGroupsQuery,
                                         _createGroupCommand,
                                         _updateGroupCommand,
                                         _deleteGroupCommand);

            _studentStore = new StudentStore(_getAllStudentsQuery,
                                             _createStudentCommand,
                                             _updateStudentCommand,
                                             _deleteStudentCommand);

            _teacherStore = new TeacherStore(_getAllTeachersQuery,
                                             _createTeacherCommand,
                                             _updateTeacherCommand,
                                             _deleteTeacherCommand);

            _modalNavigationStore = new ModalNavigationStore();
            _viewStore = new ViewStore();

            _eduPlatformViewModel = new HubViewModel(_courseStore, _groupStore, _studentStore, _teacherStore, _viewStore, _modalNavigationStore);
        }
    }

}
