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
                OnPropertyChanged(nameof(FirstName));
                OnPropertyChanged(nameof(LastName));
                OnPropertyChanged(nameof(IsEnabled));
                OnPropertyChanged(nameof(IsChecked));
            }
        }

        public Group? Group =>
            Student.Group;

        public Guid StudentId => Student.StudentId;
        public Guid? GroupId => Student.GroupId;

        public string FullName =>
             string.IsNullOrWhiteSpace(Student?.FirstName) || string.IsNullOrWhiteSpace(Student?.LastName)
             ? "<not specified>"
             : $"{Student.FirstName} {Student.LastName}";

        public string FirstName =>
            string.IsNullOrWhiteSpace(Student?.FirstName)
            ? "<not specified>" : Student.FirstName;
            
        public string LastName =>
            string.IsNullOrWhiteSpace(Student?.LastName)
            ? "<not specified>" : Student.LastName;

        public bool IsEnabled { get; set; } = false;

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
