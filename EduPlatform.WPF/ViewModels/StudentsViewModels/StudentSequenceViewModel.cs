using System.Collections.ObjectModel;
using System.Windows.Input;
using EduPlatform.Domain.Models;
using EduPlatform.WPF.Commands.StudentCommands;
using EduPlatform.WPF.Service.Utilities;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;

namespace EduPlatform.WPF.ViewModels.StudentsViewModels
{
    public class StudentSequenceViewModel : ViewModelBase, ISequenceViewModel
    {
        private readonly ObservableCollection<StudentViewModel> _studentVMs;

        public IEnumerable<StudentViewModel> StudentVMs =>
            _studentVMs.Select(svm => new StudentViewModel(svm.Student));

        public StudentViewModel? SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
                OnPropertyChanged(nameof(SelectedStudent));

                if (UpdateStudentCommand is not null)
                {
                    ((OpenUpdateStudentFormCommand)UpdateStudentCommand).UpdatingStudent = value;
                    ((OpenUpdateStudentFormCommand)UpdateStudentCommand).OnCanExecutedChanged();
                }

                if (DeleteStudentCommand is not null)
                {
                    ((DeleteStudentCommand)DeleteStudentCommand).DeletingStudent = value;
                    ((DeleteStudentCommand)DeleteStudentCommand).OnCanExecutedChanged();
                }
            }
        }

        public string? ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(HasErrorMessage));
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public bool HasErrorMessage => string.IsNullOrEmpty(_errorMessage) == false;

        public ICommand? LoadStudentsCommand { get; private set; }
        public ICommand? CreateStudentCommand { get; private set; }
        public ICommand? UpdateStudentCommand { get; private set; }
        public ICommand? DeleteStudentCommand { get; private set; }

        private GroupSequenceViewModel? _groupSequenceVM;
        private StudentViewModel? _selectedStudent;
        private readonly StudentStore _studentStore;
        private readonly ViewStore _viewStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private string? _errorMessage;

        public StudentSequenceViewModel
        (
            StudentStore studentStore,
            ViewStore viewStore,
            ModalNavigationStore modalNavigationStore
        )
        {
            _studentVMs = [];
            _studentStore = studentStore;
            _studentStore.StudentsLoaded += StudentStore_StudentsLoaded;
            _studentStore.StudentAdded += StudentStore_StudentAdded;
            _studentStore.StudentUpdated += StudentStore_StudentUpdated;
            _studentStore.StudentDeleted += StudentStore_StudentDeleted;

            _viewStore = viewStore;
            _viewStore.StudentUnfocused += ViewStore_StudentUnfocused;

            _modalNavigationStore = modalNavigationStore;
        }

        public override void Dispose()
        {
            _studentStore.StudentsLoaded -= StudentStore_StudentsLoaded;
            _studentStore.StudentAdded -= StudentStore_StudentAdded;
            _studentStore.StudentUpdated -= StudentStore_StudentUpdated;
            _studentStore.StudentDeleted -= StudentStore_StudentDeleted;
            _viewStore.StudentUnfocused -= ViewStore_StudentUnfocused;

            base.Dispose();
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

            LoadStudentsCommand = new LoadStudentsCommand(_studentStore, this);
            CreateStudentCommand =
                new OpenCreateStudentFormCommand(_studentStore, _viewStore, _modalNavigationStore, _groupSequenceVM);
            UpdateStudentCommand = new OpenUpdateStudentFormCommand
            (
                _studentStore, _selectedStudent, _viewStore,
                _modalNavigationStore, _groupSequenceVM
            );
            DeleteStudentCommand = new DeleteStudentCommand(_studentStore);
        }

        public Task? LoadStudents()
        {
            return (LoadStudentsCommand as LoadStudentsCommand)?.ExecuteAsync(null);
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

        private void UnfocusStudent()
        {
            SelectedStudent = null;
        }

        private StudentViewModel? GetStudentViewModelById(Guid studentId)
        {
            StudentViewModel? studentVM = _studentVMs
                .FirstOrDefault(svm => svm.StudentId == studentId);

            return studentVM;
        }

        private void StudentStore_StudentsLoaded()
        {
            _studentVMs.Clear();
            _studentStore.Students.ToList().ForEach(
                s =>
                {
                    Student clonedStudent = SerializationCopier.DeepCopy(s)!;
                    clonedStudent.Group = null;
                    AddStudent(clonedStudent);
                });
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
            UnfocusStudent();
        }
    }
}