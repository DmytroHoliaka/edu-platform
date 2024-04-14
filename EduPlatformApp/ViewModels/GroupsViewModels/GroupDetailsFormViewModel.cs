using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using EduPlatform.WPF.ViewModels.TeachersViewModels;
using EduPlatform.WPF.ViewModels.TeachersViewModel;
using EduPlatform.WPF.ViewModels.StudentsViewModel;
using EduPlatform.WPF.ViewModels.CoursesViewModels;

namespace EduPlatform.WPF.ViewModels.GroupsViewModels
{
    public class GroupDetailsFormViewModel : ViewModelBase
    {
        public ObservableCollection<CourseViewModel> CourseVMs { get; }
        public ObservableCollection<TeacherViewModel> TeacherVMs { get; }
        public ObservableCollection<StudentViewModel> StudentVMs { get; }

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
            CourseSequenceViewModel? courseSequenceVM,
            TeacherSequenceViewModel? teacherSequenceVM,
            StudentSequenceViewModel? studentSequenceVM,
            ICommand? submitCommand,
            ICommand? cancelCommand
        )
        {
            CourseVMs = new (courseSequenceVM?.CourseVMs ?? Enumerable.Empty<CourseViewModel>());
            TeacherVMs = new(teacherSequenceVM?.TeacherVMs ?? Enumerable.Empty<TeacherViewModel>());
            StudentVMs = new(studentSequenceVM?.StudentVMs ?? Enumerable.Empty<StudentViewModel>());
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

            base.Dispose();
        }

        private void SetupEvents()
        {
            foreach (TeacherViewModel teacher in TeacherVMs)
            {
                teacher.PropertyChanged += Teacher_PropertyChanged;
            }
        }

        private void Teacher_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TeacherViewModel.IsChecked))
            {
                OnPropertyChanged(nameof(CanSubmit));
            }
        }
    }
}
