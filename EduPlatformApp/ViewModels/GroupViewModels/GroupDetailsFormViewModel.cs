using CommunityToolkit.Mvvm.Input;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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
                OnPropertyChanged(nameof(GroupName));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        public bool CanSubmit
            => string.IsNullOrWhiteSpace(GroupName) == false
               && (TeachersFormViewModel?.Any(t => t.IsChecked == true) ?? false);

        public ICommand SubmitCommand => new RelayCommand(Submit);
        public ICommand CancelCommand => new RelayCommand(Cancel);

        public IEnumerable<TeacherFormItemViewModel> TeachersFormViewModel => _teachersFormViewModel;
        public IEnumerable<StudentFormItemViewModel> StudentsFormViewModel => _studentsFormViewModel;

        private readonly ObservableCollection<TeacherFormItemViewModel> _teachersFormViewModel;
        private readonly ObservableCollection<StudentFormItemViewModel> _studentsFormViewModel;

        private string? _groupName;


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

            foreach (var teacher in _teachersFormViewModel)
            {
                teacher.PropertyChanged += Teacher_PropertyChanged;
            }

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

        private void Teacher_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TeacherFormItemViewModel.IsChecked))
            {
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        private void Submit()
        {
            MessageBox.Show("Submit");
        }

        private void Cancel()
        {
            MessageBox.Show("Cancel");
        }
    }
}
