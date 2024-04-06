using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using EduPlatform.WPF.ViewModels.TeachersViewModels;
using EduPlatform.WPF.ViewModels.TeachersViewModel;
using EduPlatform.WPF.ViewModels.StudentsViewModel;

namespace EduPlatform.WPF.ViewModels.GroupsViewModels
{
    public class GroupDetailsFormViewModel : ViewModelBase
    {
        public ObservableCollection<TeacherViewModel> TeacherVMs { get; }
        public ObservableCollection<StudentViewModel> StudentVMs { get; }

        public Guid GroupId { get; }

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
               && (TeacherVMs?.Any(t => t.IsChecked == true) ?? false);

        public ICommand? SubmitCommand { get; }
        public ICommand? CancelCommand { get; }

        private string? _groupName;


        public GroupDetailsFormViewModel
        (
            Guid id,
            TeacherSequenceViewModel teacherSequenceVM,
            StudentSequenceViewModel studentSequenceVM,
            ICommand? submitCommand,
            ICommand? cancelCommand
        )
        {
            GroupId = id;

            TeacherVMs = new(teacherSequenceVM.TeacherVMs);
            StudentVMs = new(studentSequenceVM.StudentVMs);
            SetupEvents();

            SubmitCommand = submitCommand;
            CancelCommand = cancelCommand;
        }

        protected override void Dispose()
        {
            foreach (TeacherViewModel teacher in TeacherVMs)
            {
                teacher.PropertyChanged -= Teacher_PropertyChanged;
            }

            foreach (StudentViewModel student in StudentVMs)
            {
                student.PropertyChanged -= Student_PropertyChanged;
            }

            base.Dispose();
        }

        private void SetupEvents()
        {
            foreach (TeacherViewModel teacher in TeacherVMs)
            {
                teacher.PropertyChanged += Teacher_PropertyChanged;
            }

            foreach (StudentViewModel student in StudentVMs)
            {
                student.PropertyChanged += Student_PropertyChanged;
            }
        }

        private void Teacher_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TeacherViewModel.IsChecked))
            {
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        private void Student_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(StudentViewModel.IsChecked))
            {
                OnPropertyChanged(nameof(CanSubmit));
            }
        }
    }
}
