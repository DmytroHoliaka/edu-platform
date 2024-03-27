using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EduPlatform.WPF.ViewModels
{
    public class EduPlatformViewModel : ViewModelBase
    {
        public NavigationViewModel NavigationViewModel { get; }
        public EduPlatformOverviewViewModel EduPlatformOverviewViewModel { get; }
        public EduPlatformGroupsViewModel EduPlatformGroupsViewModel { get; }
        public EduPlatformStudentsViewModel EduPlatformStudentsViewModel { get; }
        public EduPlatformTeachersViewModel EduPlatformTeachersViewModel { get; }

        public EduPlatformViewModel()
        {
            EduPlatformOverviewViewModel = new();
            EduPlatformGroupsViewModel = new();
            EduPlatformStudentsViewModel = new();
            EduPlatformTeachersViewModel = new();
            NavigationViewModel = new();
        }
    }
}
