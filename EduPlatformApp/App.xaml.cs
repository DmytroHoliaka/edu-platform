using EduPlatform.Domain.Commands;
using EduPlatform.Domain.Queries;
using EduPlatform.EntityFramework.Commands;
using EduPlatform.EntityFramework.DbContextConfigurations;
using EduPlatform.EntityFramework.Queries;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using Microsoft.EntityFrameworkCore;
using System.Windows;

// ToDo: Complete UI 
namespace EduPlatform.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly EduPlatformDbContextFactory _eduPlatformDbContextFactory;

        private readonly IGetAllCoursesQuery _getAllCoursesQuery;
        private readonly ICreateCourseCommand _createCourseCommand;
        private readonly IUpdateCourseCommand _updateCourseCommand;
        private readonly IDeleteCourseCommand _deleteCourseCommand;

        private readonly IGetAllGroupsQuery _getAllGroupsQuery;
        private readonly ICreateGroupCommand _createGroupCommand;
        private readonly IUpdateGroupCommand _updateGroupCommand;
        private readonly IDeleteGroupCommand _deleteGroupCommand;

        private readonly IGetAllStudentsQuery _getAllStudentsQuery;
        private readonly ICreateStudentCommand _createStudentCommand;
        private readonly IUpdateStudentCommand _updateStudentCommand;
        private readonly IDeleteStudentCommand _deleteStudentCommand;

        private readonly IGetAllTeachersQuery _getAllTeachersQuery;
        private readonly ICreateTeacherCommand _createTeacherCommand;
        private readonly IUpdateTeacherCommand _updateTeacherCommand;
        private readonly IDeleteTeacherCommand _deleteTeacherCommand;

        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly CourseStore _courseStore;
        private readonly GroupStore _groupStore;
        private readonly StudentStore _studentStore;
        private readonly TeacherStore _teacherStore;
        private readonly ViewStore _viewStore;
        private readonly HubViewModel _eduPlatformViewModel;

        public App()
        {
            // ToDo: Decompose constructor
            // ToDo: Use JSON configuration file
            string connectionString = @"Server=(localdb)\mssqllocaldb;Database=EduPlatform;Trusted_Connection=True;";

            DbContextOptions options = new DbContextOptionsBuilder().UseSqlServer(connectionString).Options;
            _eduPlatformDbContextFactory = new(options);

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

            _courseStore = new(_getAllCoursesQuery,
                               _createCourseCommand,
                               _updateCourseCommand,
                               _deleteCourseCommand);

            _groupStore = new(_getAllGroupsQuery,
                              _createGroupCommand,
                              _updateGroupCommand,
                              _deleteGroupCommand);

            _studentStore = new(_getAllStudentsQuery,
                                _createStudentCommand,
                                _updateStudentCommand,
                                _deleteStudentCommand);

            _teacherStore = new(_getAllTeachersQuery,
                                _createTeacherCommand,
                                _updateTeacherCommand,
                                _deleteTeacherCommand);

            _modalNavigationStore = new();
            _viewStore = new();

            _eduPlatformViewModel = new(_courseStore, _groupStore, _studentStore, _teacherStore, _viewStore, _modalNavigationStore);

            // ToDo: Remove this lines
            //EduPlatformDbContext context = _eduPlatformDbContextFactory.Create();
            //context.Database.EnsureDeleted();
            //context.Dispose();
        }

        //public App(EduPlatformDbContextFactory eduPlatformDbContextFactory)
        //{
        //    _eduPlatformDbContextFactory = eduPlatformDbContextFactory;
        //}

        protected override void OnStartup(StartupEventArgs e)
        {
            using (EduPlatformDbContext context = _eduPlatformDbContextFactory.Create())
            {
                context.Database.Migrate();
            }

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
        }
    }

}
