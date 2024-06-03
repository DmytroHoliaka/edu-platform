using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using EduPlatform.WPF.ViewModels.TeachersViewModels;
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
            get => _groupName;

            set
            {
                _groupName = value;
                OnPropertyChanged(nameof(GroupName));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        public string? ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(HasErrorMessage));
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public bool CanSubmit
            => string.IsNullOrWhiteSpace(GroupName) == false
               && (TeacherVMs.Any(t => t.IsChecked));

        public bool HasErrorMessage => string.IsNullOrEmpty(_errorMessage) == false;

        public ICommand? SubmitCommand { get; }
        public ICommand? CancelCommand { get; }

        private string? _groupName;
        private readonly GroupViewModel? _selectedGroup;
        private string? _errorMessage;

        public GroupDetailsFormViewModel
        (
            GroupViewModel? selectedGroup,
            CourseSequenceViewModel? courseSequenceVM,
            TeacherSequenceViewModel? teacherSequenceVM,
            StudentSequenceViewModel? studentSequenceVM,
            ICommand? submitCommand,
            ICommand? cancelCommand
        )
        {
            _selectedGroup = selectedGroup;
            CourseVMs = new ObservableCollection<CourseViewModel>(
                courseSequenceVM?.CourseVMs ?? Enumerable.Empty<CourseViewModel>());
            TeacherVMs = new ObservableCollection<TeacherViewModel>(
                teacherSequenceVM?.TeacherVMs ?? Enumerable.Empty<TeacherViewModel>());
            StudentVMs = new ObservableCollection<StudentViewModel>(
                studentSequenceVM?.StudentVMs ?? Enumerable.Empty<StudentViewModel>());

            SetMarkers();
            SetupEvents();

            SubmitCommand = submitCommand;
            CancelCommand = cancelCommand;
        }

        public override void Dispose()
        {
            foreach (TeacherViewModel teacher in TeacherVMs)
            {
                teacher.PropertyChanged -= Teacher_PropertyChanged;
            }

            base.Dispose();
        }

        private void SetMarkers()
        {
            CourseVMs.ToList().ForEach(cvm => cvm.IsEnabled = true);
            TeacherVMs.ToList().ForEach(tvm => tvm.IsEnabled = true);

            if (_selectedGroup is not null)
            {
                TeacherVMs.Where(
                        tvm => tvm.Groups.FirstOrDefault(i => i.GroupId == _selectedGroup.GroupId)?.Teachers.Count <= 1)
                    .ToList().ForEach(tvm => tvm.IsEnabled = false);
            }

            StudentVMs.Where(svm => svm.IsChecked || svm.Group is null)
                .ToList().ForEach(svm => svm.IsEnabled = true);
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