using EduPlatform.WPF.Commands;
using EduPlatform.WPF.Models;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using EduPlatform.WPF.ViewModels.TeachersViewModel;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace EduPlatform.WPF.ViewModels.StudentsViewModel
{
    public class StudentSequenceViewModel : ViewModelBase
    {
        private readonly ObservableCollection<StudentViewModel> _studentVMs;
        public IEnumerable<StudentViewModel> StudentVMs =>
            _studentVMs.Select(s => new StudentViewModel(s));

        public StudentViewModel? SelectedStudent { get; set; }

        public ICommand CreateGroupCommand { get; private set; }
        public ICommand UpdateGroupCommand { get; private set; }
        public ICommand DeleteGroupCommand { get; private set; }

        private GroupSequenceViewModel? _groupSequenceVM;
        private readonly GroupStore _groupStore;
        private readonly ViewStore _viewStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public StudentSequenceViewModel
        (
            GroupStore groupStore,
            ViewStore viewStore,
            ModalNavigationStore modalNavigationStore
        )
        {
            _studentVMs = [];
            _groupStore = groupStore;
            _viewStore = viewStore;
            _modalNavigationStore = modalNavigationStore;

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

            _studentVMs.Add(new StudentViewModel(student1));
            _studentVMs.Add(new StudentViewModel(student2));
        }

        public void SetGroupSequence(GroupSequenceViewModel newGroup)
        {
            _groupSequenceVM = newGroup;
        }

        public void ConfigureCommands()
        {
            CreateGroupCommand = new OpenCreateStudentFormCommand(_groupStore, _viewStore, _modalNavigationStore, _groupSequenceVM);
            UpdateGroupCommand = null;
            DeleteGroupCommand = null;
        }
    }
}


//Student student1 = new()
//{
//    StudentId = Guid.NewGuid(),
//    FirstName = "John",
//    LastName = "Student",
//};

//Student student2 = new()
//{
//    StudentId = Guid.NewGuid(),
//    FirstName = "Alex",
//    LastName = "Student",
//};

//Student student3 = new()
//{
//    StudentId = Guid.NewGuid(),
//    FirstName = "Rick",
//    LastName = "Student",
//};

//Student student4 = new()
//{
//    StudentId = Guid.NewGuid(),
//    FirstName = "Thomas",
//    LastName = "Student",
//};

//Student student5 = new()
//{
//    StudentId = Guid.NewGuid(),
//    FirstName = "Richard",
//    LastName = "Student",
//};

//_studentVMs.Add(new StudentViewModel(student1));
//_studentVMs.Add(new StudentViewModel(student2));
//_studentVMs.Add(new StudentViewModel(student3));
//_studentVMs.Add(new StudentViewModel(student4));
//_studentVMs.Add(new StudentViewModel(student5));