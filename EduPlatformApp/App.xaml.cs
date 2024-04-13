using EduPlatform.WPF.Service;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using System.Windows;

namespace EduPlatform.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly CourseStore _courseStore;
        private readonly GroupStore _groupStore;
        private readonly StudentStore _studentStore;
        private readonly TeacherStore _teacherStore;
        private readonly ViewStore _viewStore;
        private readonly HubViewModel _eduPlatformViewModel;

        public App()
        {
            _modalNavigationStore = new();
            _courseStore = new();
            _groupStore = new();
            _studentStore = new();
            _teacherStore = new();
            _viewStore = new();

            _eduPlatformViewModel = new(_courseStore, _groupStore, _studentStore, _teacherStore, _viewStore, _modalNavigationStore);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
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
