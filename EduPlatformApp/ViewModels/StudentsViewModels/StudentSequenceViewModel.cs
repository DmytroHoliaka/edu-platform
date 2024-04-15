using EduPlatform.WPF.Commands.StudentCommands;
using EduPlatform.WPF.Models;
using EduPlatform.WPF.Pages;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using System.Collections.ObjectModel;
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

        private void AddStudent(Student student)
        {
            _studentVMs.Add(new StudentViewModel(student));
        }

        private void UpdateStudent(Student targetStudent)
        {
            StudentViewModel sourceStudentVM = GetStudentViewModelById(targetStudent.StudentId)!;

            sourceStudentVM.Student = targetStudent;
        }

        private void DeleteStudent(Guid sourceId)
        {
            StudentViewModel deletingStudentVM = GetStudentViewModelById(sourceId)!;

            _studentVMs.Remove(deletingStudentVM);
        }

        private void RefreshDependenciesOnAdding(Student newStudent)
        {
            newStudent.Group?.Students.Add(newStudent);
        }

        private void RefreshDependenciesOnUpdating(Student targetStudent)
        {
            StudentViewModel sourceStudentVM = GetStudentViewModelById(targetStudent.StudentId)!;

            sourceStudentVM.Group?.Students.Remove(sourceStudentVM.Student);
            targetStudent.Group?.Students.Add(targetStudent);
        }

        private void RefreshDependenciesOnDeleting(Guid studentId)
        {
            StudentViewModel sourceStudentVM = GetStudentViewModelById(studentId)!;
            sourceStudentVM.Student.Group?.Students.Remove(sourceStudentVM.Student);
        }

        private void UnfocuseStudent()
        {
            SelectedStudent = null;
        }

        private StudentViewModel? GetStudentViewModelById(Guid studentId)
        {
            StudentViewModel? studentVM = _studentVMs
                .FirstOrDefault(svm => svm.StudentId == studentId);

            return studentVM;
        }

        // ToDo: Remove
        public void InsertTestData()
        {
            Student student1 = new()
            {
                StudentId = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Student",
                Group = _groupSequenceVM.GroupVMs.ElementAt(0).Group,
                GroupId = _groupSequenceVM.GroupVMs.ElementAt(0).GroupId,
            };

            Student student2 = new()
            {
                StudentId = Guid.NewGuid(),
                FirstName = "Alex",
                LastName = "Student",
                Group = _groupSequenceVM.GroupVMs.ElementAt(1).Group,
                GroupId = _groupSequenceVM.GroupVMs.ElementAt(1).GroupId,
            };

            Student student3 = new()
            {
                StudentId = Guid.NewGuid(),
                FirstName = "Dmytro",
                LastName = "Student",
                Group = _groupSequenceVM.GroupVMs.ElementAt(2).Group,
                GroupId = _groupSequenceVM.GroupVMs.ElementAt(2).GroupId,
            };

            Student student4 = new()
            {
                StudentId = Guid.NewGuid(),
                FirstName = "Richard",
                LastName = "Student",
                Group = null,
                GroupId = null,
            };

            _studentVMs.Add(new StudentViewModel(student1));
            _studentVMs.Add(new StudentViewModel(student2));
            _studentVMs.Add(new StudentViewModel(student3));
            _studentVMs.Add(new StudentViewModel(student4));
        }

        private void StudentStore_StudentAdded(Student student)
        {
            RefreshDependenciesOnAdding(student);
            AddStudent(student);
            OnPropertyChanged(nameof(StudentVMs));
        }

        private void StudentStore_StudentUpdated(Student targetStudent)
        {
            RefreshDependenciesOnUpdating(targetStudent);
            UpdateStudent(targetStudent);
            OnPropertyChanged(nameof(StudentVMs));
        }

        private void StudentStore_StudentDeleted(Guid studentId)
        {
            RefreshDependenciesOnDeleting(studentId);
            DeleteStudent(studentId);
            OnPropertyChanged(nameof(StudentVMs));
        }

        private void ViewStore_StudentUnfocused()
        {
            UnfocuseStudent();
        }
    }
}