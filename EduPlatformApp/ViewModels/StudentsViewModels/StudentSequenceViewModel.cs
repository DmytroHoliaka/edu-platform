using EduPlatform.WPF.Commands;
using EduPlatform.WPF.Models;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace EduPlatform.WPF.ViewModels.StudentsViewModel
{
    public class StudentSequenceViewModel : ViewModelBase
    {
        private readonly ObservableCollection<StudentViewModel> _studentVMs;
        public IEnumerable<StudentViewModel> StudentVMs =>
            _studentVMs.Select(svm => new StudentViewModel(svm.Student));

        public StudentViewModel? SelectedStudent
        {
            get
            {
                return _selectedStudent;
            }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged(nameof(SelectedStudent));

                ((OpenUpdateStudentFormCommand)UpdateStudentCommand).UpdatingStudent = value;
                ((OpenUpdateStudentFormCommand)UpdateStudentCommand).OnCanExecutedChanded();

                ((DeleteStudentCommand)DeleteStudentCommand).DeletingStudent = value;
                ((DeleteStudentCommand)DeleteStudentCommand).OnCanExecutedChanded();
            }
        }

        public ICommand CreateStudentCommand { get; private set; }
        public ICommand UpdateStudentCommand { get; private set; }
        public ICommand DeleteStudentCommand { get; private set; }

        private GroupSequenceViewModel? _groupSequenceVM;
        private StudentViewModel? _selectedStudent;
        private readonly StudentStore _studentStore;
        private readonly ViewStore _viewStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public StudentSequenceViewModel
        (
            StudentStore studentStore,
            ViewStore viewStore,
            ModalNavigationStore modalNavigationStore
        )
        {
            _studentVMs = [];
            _studentStore = studentStore;
            _studentStore.StudentAdded += StudentStore_StudentAdded;
            _studentStore.StudentUpdated += StudentStore_StudentUpdated;
            _studentStore.StudentDeleted += StudentStore_StudentDeleted;

            _viewStore = viewStore;
            _viewStore.StudentUnfocused += ViewStore_StudentUnfocused;

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
            if (_groupSequenceVM is null)
            {
                return;
            }

            CreateStudentCommand = new OpenCreateStudentFormCommand(_studentStore, _viewStore, _modalNavigationStore, _groupSequenceVM);
            UpdateStudentCommand = new OpenUpdateStudentFormCommand(_studentStore, _selectedStudent, _viewStore, _modalNavigationStore, _groupSequenceVM);
            DeleteStudentCommand = new DeleteStudentCommand(_studentStore, _modalNavigationStore);
        }

        public void AddStudent(Student? student)
        {
            if (student is null)
            {
                return;
            }

            _studentVMs.Add(new StudentViewModel(student));
            OnPropertyChanged(nameof(StudentVMs));
        }

        private void UpdateStudent(Guid sourceId, Student? targetStudent)
        {
            if (targetStudent is null)
            {
                return;
            }

            StudentViewModel? sourceStudent = _studentVMs.FirstOrDefault(svm => svm.StudentId == targetStudent.StudentId);

            if (sourceStudent is null)
            {
                return;
            }

            sourceStudent.Student = targetStudent;
            OnPropertyChanged(nameof(StudentVMs));
        }

        private void DeleteStudent(Guid sourceId)
        {
            StudentViewModel? sourceStudent = _studentVMs.FirstOrDefault(svm => svm.StudentId == sourceId);

            if (sourceStudent is null)
            {
                return;
            }

            _studentVMs.Remove(sourceStudent);
            OnPropertyChanged(nameof(StudentVMs));
        }


        public void UnfocuseStudent()
        {
            SelectedStudent = null;
        }

        private void StudentStore_StudentAdded(Student student)
        {
            AddStudent(student);
        }

        private void StudentStore_StudentUpdated(Guid sourceId, Student targetStudent)
        {
            UpdateStudent(sourceId, targetStudent);
        }

        private void StudentStore_StudentDeleted(Guid sourceId)
        {
            DeleteStudent(sourceId);
        }

        private void ViewStore_StudentUnfocused()
        {
            UnfocuseStudent();
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