using EduPlatform.WPF.Models;
using EduPlatform.WPF.ViewModels.GeneralViewModels;

namespace EduPlatform.WPF.ViewModels.TeachersViewModels
{
    public class TeacherViewModel : ViewModelBase
    {
        public Teacher Teacher
        {
            get
            {
                return _teacher!;
            }
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
            }
        }

        public Guid TeacherId => 
            Teacher.TeacherId;

        public IEnumerable<Group> Groups =>
            Teacher.Groups ?? Enumerable.Empty<Group>();

        public string FullName =>
            Teacher.FirstName != null || Teacher.LastName != null
            ? $"{Teacher.FirstName} {Teacher.LastName}"
            : "Unknown";

        public string FirstName =>
            Teacher?.FirstName ?? "<unknown>";

        public string LastName =>
            Teacher?.LastName ?? "<unknown>";

        public bool IsChecked
        {
            get
            {
                return _isChecked;
            }
            set
            {
                _isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }

        private bool _isChecked;
        private Teacher? _teacher;

        public TeacherViewModel(Teacher teacher)
        {
            Teacher = teacher;
            IsChecked = false;
        }
    }
}
