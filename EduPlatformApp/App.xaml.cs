using EduPlatform.WPF.ViewModels.GeneralViewModels;
using System.Windows;

namespace EduPlatform.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new EduPlatformViewModel()
            };

            MainWindow.Show();
            base.OnStartup(e);
        }
    }

}
