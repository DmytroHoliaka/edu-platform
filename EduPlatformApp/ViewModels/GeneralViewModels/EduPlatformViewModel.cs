using EduPlatform.WPF.ViewModels.GeneralViewModels.OverviewViewModel;
using EduPlatform.WPF.ViewModels.GroupViewModels;
using EduPlatform.WPF.ViewModels.NavigationsViewModel;
using EduPlatform.WPF.ViewModels.StudentsViewModel;
using EduPlatform.WPF.ViewModels.TeacherViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EduPlatform.WPF.ViewModels.GeneralViewModels
{
    public class EduPlatformViewModel : ViewModelBase
    {
        public NavigationViewModel NavigationViewModel { get; }
        public EduPlatformOverviewViewModel EduPlatformOverviewViewModel { get; }
        public EduPlatformGroupsViewModel EduPlatformGroupsViewModel { get; }
        public EduPlatformStudentsViewModel EduPlatformStudentsViewModel { get; }
        public EduPlatformTeachersViewModel EduPlatformTeachersViewModel { get; }
        public GroupDetailsFormViewModel GroupDetailsFormViewModel { get; }

        public EduPlatformViewModel()
        {
            EduPlatformOverviewViewModel = new();
            EduPlatformGroupsViewModel = new();
            EduPlatformStudentsViewModel = new();
            EduPlatformTeachersViewModel = new();
            NavigationViewModel = new();
            GroupDetailsFormViewModel = new();
        }
    }
}
