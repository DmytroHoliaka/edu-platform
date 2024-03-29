using EduPlatform.WPF.ViewModels.GeneralViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EduPlatform.WPF.ViewModels.GroupViewModels
{
    public class GroupDetailsFormViewModel : ViewModelBase
    {

        public string? GroupName
        {
            get
            {
                return _groupName;
            }

            set
            {
                _groupName = value;
                OnPropertyChanged(nameof(_groupName));
            }
        }

        public IEnumerable<TeacherFormItemViewModel>? TeachersFormViewModel => _teachersFormViewModel;
        public IEnumerable<StudentFormItemViewModel>? StudentsFormViewModel => _studentsFormViewModel;

        private string? _groupName;
        private readonly ObservableCollection<TeacherFormItemViewModel> _teachersFormViewModel;
        private readonly ObservableCollection<StudentFormItemViewModel> _studentsFormViewModel;


        public GroupDetailsFormViewModel()
        {
            _teachersFormViewModel =
            [
                new("Dmytro Teacher"),
                new("Alex Teacher"),
                new("John Teacher"),
                new("Robert Teacher"),
                new("Carl Teacher"),
                new("Donald Teacher"),
                new("Jake Teacher"),
                new("Limber Teacher"),
            ];

            _studentsFormViewModel =
           [
               new("Dmytro Student"),
               new("Alex Student"),
               new("John Student"),
               new("Robert Student"),
               new("Carl Student"),
               new("Donald Student"),
               new("Jake Student"),
               new("Limber Student"),
           ];
        }

    }
}
