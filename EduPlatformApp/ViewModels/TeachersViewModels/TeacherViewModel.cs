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
                OnPropertyChanged(nameof(FullName));
                OnPropertyChanged(nameof(IsChecked));
            }
        }

        public Guid TeacherId => Teacher.TeacherId;

        public string FullName =>
            Teacher.FirstName != null || Teacher.LastName != null
            ? $"{Teacher.FirstName} {Teacher.LastName}"
            : "Unknown";

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
