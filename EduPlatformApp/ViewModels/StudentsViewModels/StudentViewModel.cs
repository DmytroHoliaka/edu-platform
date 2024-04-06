using EduPlatform.WPF.Models;
using EduPlatform.WPF.ViewModels.GeneralViewModels;

namespace EduPlatform.WPF.ViewModels.StudentsViewModels
{
    public class StudentViewModel : ViewModelBase
    {
        public Student Student
        {
            get
            {
                return _student!;
            }
            set
            {
                _student = value;
                OnPropertyChanged(nameof(Student));
                OnPropertyChanged(nameof(StudentId));
                OnPropertyChanged(nameof(FullName));
                OnPropertyChanged(nameof(IsChecked));
            }
        }

        public Guid StudentId => Student.StudentId;

        public string FullName =>
             Student.FirstName != null || Student.LastName != null
             ? $"{Student.FirstName} {Student.LastName}"
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
        private Student? _student;

        public StudentViewModel(Student student)
        {
            Student = student;
            IsChecked = false;
        }
    }
}
