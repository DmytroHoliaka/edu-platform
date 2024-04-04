using EduPlatform.WPF.Models;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using System.Collections.ObjectModel;

namespace EduPlatform.WPF.ViewModels.StudentsViewModel
{
    public class StudentSequenceViewModel : ViewModelBase
    {
        private readonly ObservableCollection<StudentViewModel> _studentVMs;
        public IEnumerable<StudentViewModel> StudentVMs =>
            _studentVMs.Select(s => new StudentViewModel(s));

        public StudentSequenceViewModel()
        {
            _studentVMs = [];

            Student student1 = new()
            {
                StudentId = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Student",
            };

            Student student2 = new()
            {
                StudentId = Guid.NewGuid(),
                FirstName = "Alex",
                LastName = "Student",
            };

            Student student3 = new()
            {
                StudentId = Guid.NewGuid(),
                FirstName = "Rick",
                LastName = "Student",
            };

            Student student4 = new()
            {
                StudentId = Guid.NewGuid(),
                FirstName = "Thomas",
                LastName = "Student",
            };

            Student student5 = new()
            {
                StudentId = Guid.NewGuid(),
                FirstName = "Richard",
                LastName = "Student",
            };

            _studentVMs.Add(new StudentViewModel(student1));
            _studentVMs.Add(new StudentViewModel(student2));
            _studentVMs.Add(new StudentViewModel(student3));
            _studentVMs.Add(new StudentViewModel(student4));
            _studentVMs.Add(new StudentViewModel(student5));
        }
    }
}
