using EduPlatform.WPF.Models;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.TeachersViewModels;
using System.Collections.ObjectModel;

namespace EduPlatform.WPF.ViewModels.TeachersViewModel
{
    public class TeacherSequenceViewModel : ViewModelBase
    {
        private readonly ObservableCollection<TeacherViewModel> _teacherVMs;
        public IEnumerable<TeacherViewModel> TeacherVMs =>
            _teacherVMs.Select(t => new TeacherViewModel(t));


        public TeacherSequenceViewModel()
        {
            _teacherVMs = [];

            Teacher teacher1 = new()
            {
                TeacherId = Guid.NewGuid(),
                FirstName = "Dmytro",
                LastName = "Teacher",
            };

            Teacher teacher2 = new()
            {
                TeacherId = Guid.NewGuid(),
                FirstName = "Alex",
                LastName = "Teacher",
            };

            Teacher teacher3 = new()
            {
                TeacherId = Guid.NewGuid(),
                FirstName = "Rick",
                LastName = "Teacher",
            };

            Teacher teacher4 = new()
            {
                TeacherId = Guid.NewGuid(),
                FirstName = "Thomas",
                LastName = "Teacher",
            };

            Teacher teacher5 = new()
            {
                TeacherId = Guid.NewGuid(),
                FirstName = "Richard",
                LastName = "Teacher",
            };

            _teacherVMs.Add(new TeacherViewModel(teacher1));
            _teacherVMs.Add(new TeacherViewModel(teacher2));
            _teacherVMs.Add(new TeacherViewModel(teacher3));
            _teacherVMs.Add(new TeacherViewModel(teacher4));
            _teacherVMs.Add(new TeacherViewModel(teacher5));
        }
    }
}
