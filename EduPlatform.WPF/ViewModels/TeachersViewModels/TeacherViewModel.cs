using EduPlatform.Domain.Models;
using EduPlatform.WPF.ViewModels.GeneralViewModels;

namespace EduPlatform.WPF.ViewModels.TeachersViewModels
{
    public class TeacherViewModel : ViewModelBase
    {
        public Teacher Teacher
        {
            get => _teacher!;
            set
            {
                _teacher = value;
                OnPropertyChanged(nameof(Teacher));
                OnPropertyChanged(nameof(TeacherId));
                OnPropertyChanged(nameof(Groups));
                OnPropertyChanged(nameof(FullName));
                OnPropertyChanged(nameof(FirstName));
                OnPropertyChanged(nameof(LastName));
                OnPropertyChanged(nameof(IsChecked));
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
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

        public string FullName =>
            string.IsNullOrWhiteSpace(Teacher.FirstName) || string.IsNullOrWhiteSpace(Teacher.LastName)
                ? "<not specified>"
                : $"{Teacher.FirstName} {Teacher.LastName}";

        public string FirstName =>
            string.IsNullOrWhiteSpace(Teacher.FirstName)
                ? "<not specified>" : Teacher.FirstName;

        public string LastName =>
            string.IsNullOrWhiteSpace(Teacher.LastName)
                ? "<not specified>" : Teacher.LastName;

        public bool HasErrorMessage => string.IsNullOrEmpty(_errorMessage) == false;
        public Guid TeacherId => Teacher.TeacherId;
        public IEnumerable<Group> Groups => Teacher.Groups;

        public bool IsEnabled { get; set; } = false;

        private bool _isChecked;
        private Teacher? _teacher;
        private string? _errorMessage;

        public TeacherViewModel(Teacher teacher)
        {
            Teacher = teacher;
            IsChecked = false;
        }
    }
}
