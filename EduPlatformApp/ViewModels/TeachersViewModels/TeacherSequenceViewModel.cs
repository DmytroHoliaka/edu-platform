using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Commands.StudentCommands;
using EduPlatform.WPF.Commands.TeacherCommands;
using EduPlatform.WPF.Models;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using EduPlatform.WPF.ViewModels.TeachersViewModels;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace EduPlatform.WPF.ViewModels.TeachersViewModel
{
    public class TeacherSequenceViewModel : ViewModelBase
    {
        private readonly ObservableCollection<TeacherViewModel> _teacherVMs;
        public IEnumerable<TeacherViewModel> TeacherVMs =>
            _teacherVMs.Select(tvm => new TeacherViewModel(tvm.Teacher));

        public TeacherViewModel? SelectedTeacher
        {
            get
            {
                return _selectedTeacher;
            }
            set
            {
                _selectedTeacher = value;
                OnPropertyChanged(nameof(SelectedTeacher));

                ((OpenUpdateTeacherFormCommand)UpdateTeacherCommand).UpdatingTeacher = value;
                ((OpenUpdateTeacherFormCommand)UpdateTeacherCommand).OnCanExecutedChanded();

                ((DeleteTeacherCommand)DeleteTeacherCommand).DeletingTeacher = value;
                ((DeleteTeacherCommand)DeleteTeacherCommand).OnCanExecutedChanded();
            }
        }

        public ICommand CreateTeacherCommand { get; private set; }
        public ICommand UpdateTeacherCommand { get; private set; }
        public ICommand DeleteTeacherCommand { get; private set; }

        private GroupSequenceViewModel? _groupSequenceVM;
        private TeacherViewModel? _selectedTeacher;
        private readonly TeacherStore _teacherStore;
        private readonly ViewStore _viewStore;
        private readonly ModalNavigationStore _modalNavigationStore;


        public TeacherSequenceViewModel
        (
            TeacherStore teacherStore,
            ViewStore viewStore,
            ModalNavigationStore modalNavigationStore
        )
        {
            _teacherVMs = [];
            _teacherStore = teacherStore;
            _teacherStore.TeacherAdded += TeacherStore_TeacherAdded;
            _teacherStore.TeacherUpdated += TeacherStore_TeacherUpdated;
            _teacherStore.TeacherDeleted += TeacherStore_TeacherDeleted;

            _viewStore = viewStore;
            _viewStore.TeacherUnfocused += ViewStore_TeacherUnfocused;

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

            CreateTeacherCommand = new OpenCreateTeacherFormCommand(_teacherStore,
                                                                    _viewStore,
                                                                    _modalNavigationStore,
                                                                    _groupSequenceVM);

            UpdateTeacherCommand = new OpenUpdateTeacherFormCommand(_teacherStore,
                                                                    _selectedTeacher,
                                                                    _viewStore,
                                                                    _modalNavigationStore,
                                                                    _groupSequenceVM);
            
            DeleteTeacherCommand = new DeleteTeacherCommand(_teacherStore,
                                                            _modalNavigationStore);
        }

        // ToDo: Remove
        public void InsertTestData()
        {
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

        private TeacherViewModel? GetTeacherViewModelById(Guid id)
        {
            TeacherViewModel? teacher = _teacherVMs.FirstOrDefault(tvm => tvm.TeacherId == id);
            return teacher;
        }

        private void AddTeacher(Teacher teacher)
        {
            TeacherViewModel teacherVM = new(teacher);
            _teacherVMs.Add(teacherVM);
        }

        private void UpdateTeacher(Teacher targetTeacher)
        {
            Guid targetId = targetTeacher.TeacherId;
            TeacherViewModel sourceTeacher = GetTeacherViewModelById(targetId)!;
            sourceTeacher.Teacher = targetTeacher;
        }

        private void DeleteTeacher(Guid teacherId)
        {
            TeacherViewModel teacher = GetTeacherViewModelById(teacherId)!;
            _teacherVMs.Remove(teacher);
        }

        private void UnfocuseTeacher()
        {
            SelectedTeacher = null;
        }

        private void TeacherStore_TeacherAdded(Teacher teacher)
        {
            AddTeacher(teacher);
            OnPropertyChanged(nameof(TeacherVMs));
        }

        private void TeacherStore_TeacherUpdated(Teacher targetTeacher)
        {
            UpdateTeacher(targetTeacher);
            OnPropertyChanged(nameof(TeacherVMs));
        }

        private void TeacherStore_TeacherDeleted(Guid teacherId)
        {
            DeleteTeacher(teacherId);
            OnPropertyChanged(nameof(TeacherVMs));
        }

        private void ViewStore_TeacherUnfocused()
        {
            UnfocuseTeacher();
        }
    }
}
