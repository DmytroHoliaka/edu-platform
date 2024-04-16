using EduPlatform.Domain.Models;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using System.Collections.ObjectModel;

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
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        public Guid TeacherId => 
            Teacher.TeacherId;

        public IEnumerable<Group> Groups =>
            Teacher.Groups ?? Enumerable.Empty<Group>();

        public string FullName =>
             string.IsNullOrWhiteSpace(Teacher?.FirstName) || string.IsNullOrWhiteSpace(Teacher?.LastName)
             ? "<not specified>"
             : $"{Teacher.FirstName} {Teacher.LastName}";

        public string FirstName =>
            string.IsNullOrWhiteSpace(Teacher?.FirstName)
            ? "<not specified>" : Teacher.FirstName;

        public string LastName =>
            string.IsNullOrWhiteSpace(Teacher?.LastName)
            ? "<not specified>" : Teacher.LastName;

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

        public bool IsEnabled { get; set; } = false;

        private bool _isChecked;
        private Teacher? _teacher;

        public TeacherViewModel(Teacher teacher)
        {
            Teacher = teacher;
            IsChecked = false;
        }
    }
}
