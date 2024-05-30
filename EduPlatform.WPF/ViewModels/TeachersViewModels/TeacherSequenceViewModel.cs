using EduPlatform.WPF.Commands.TeacherCommands;
using EduPlatform.Domain.Models;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.TeachersViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EduPlatform.WPF.Service;

namespace EduPlatform.WPF.ViewModels.TeachersViewModel
{
    public class TeacherSequenceViewModel : ViewModelBase, ISequenceViewModel
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
                ((OpenUpdateTeacherFormCommand)UpdateTeacherCommand).OnCanExecutedChanged();

                ((DeleteTeacherCommand)DeleteTeacherCommand).DeletingTeacher = value;
                ((DeleteTeacherCommand)DeleteTeacherCommand).OnCanExecutedChanged();
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

        public ICommand LoadTeachersCommand { get; private set; }
        public ICommand CreateTeacherCommand { get; private set; }
        public ICommand UpdateTeacherCommand { get; private set; }
        public ICommand DeleteTeacherCommand { get; private set; }

        private GroupSequenceViewModel? _groupSequenceVM;
        private TeacherViewModel? _selectedTeacher;
        private readonly TeacherStore _teacherStore;
        private readonly ViewStore _viewStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private string? _errorMessage;

        public TeacherSequenceViewModel
        (
            TeacherStore teacherStore,
            ViewStore viewStore,
            ModalNavigationStore modalNavigationStore
        )
        {
            _teacherVMs = [];
            _teacherStore = teacherStore;
            _teacherStore.TeachersLoaded += TeacherStore_TeachersLoaded;
            _teacherStore.TeacherAdded += TeacherStore_TeacherAdded;
            _teacherStore.TeacherUpdated += TeacherStore_TeacherUpdated;
            _teacherStore.TeacherDeleted += TeacherStore_TeacherDeleted;

            _viewStore = viewStore;
            _viewStore.TeacherUnfocused += ViewStore_TeacherUnfocused;

            _modalNavigationStore = modalNavigationStore;
        }

        public override void Dispose()
        {
            _teacherStore.TeachersLoaded -= TeacherStore_TeachersLoaded;
            _teacherStore.TeacherAdded -= TeacherStore_TeacherAdded;
            _teacherStore.TeacherUpdated -= TeacherStore_TeacherUpdated;
            _teacherStore.TeacherDeleted -= TeacherStore_TeacherDeleted;
            _viewStore.TeacherUnfocused -= ViewStore_TeacherUnfocused;
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

            LoadTeachersCommand = new LoadTeachersCommand(_teacherStore, this);

            CreateTeacherCommand = new OpenCreateTeacherFormCommand(_teacherStore,
                                                                    _viewStore,
                                                                    _modalNavigationStore,
                                                                    _groupSequenceVM);

            UpdateTeacherCommand = new OpenUpdateTeacherFormCommand(_teacherStore,
                                                                    _selectedTeacher,
                                                                    _viewStore,
                                                                    _modalNavigationStore,
                                                                    _groupSequenceVM);
            
            DeleteTeacherCommand = new DeleteTeacherCommand(_teacherStore);
        }

        private void RefreshDependenciesOnAdding(Teacher newTeacher)
        {
            newTeacher.Groups.ToList()
                .ForEach(g => g.Teachers.Add(newTeacher));
        }

        private void RefreshDependenciesOnUpdating(Teacher targetTeacher)
        {
            TeacherViewModel sourceTeacherVM = GetTeacherViewModelById(targetTeacher.TeacherId)!;

            sourceTeacherVM.Groups.ToList()
                .ForEach(g => g.Teachers.Remove(sourceTeacherVM.Teacher));

            targetTeacher.Groups.ToList()
                .ForEach(g => g.Teachers.Add(targetTeacher));
        }

        private void RefreshDependenciesOnDeleting(Guid teacherId)
        {
            TeacherViewModel deletingTeacherVM = GetTeacherViewModelById(teacherId)!;

            deletingTeacherVM.Teacher.Groups
                .ToList().ForEach(g => g.Teachers.Remove(deletingTeacherVM.Teacher));
        }

        private TeacherViewModel? GetTeacherViewModelById(Guid id)
        {
            TeacherViewModel? teacherVM = _teacherVMs.FirstOrDefault(tvm => tvm.TeacherId == id);
            return teacherVM;
        }

        public Task LoadTeachers()
        {
            return ((LoadTeachersCommand)LoadTeachersCommand).ExecuteAsync(null);
        }

        private void AddTeacher(Teacher teacher)
        {
            TeacherViewModel teacherVM = new(teacher);
            _teacherVMs.Add(teacherVM);
        }

        private void UpdateTeacher(Teacher targetTeacher)
        {
            Guid targetId = targetTeacher.TeacherId;
            TeacherViewModel sourceTeacherVM = GetTeacherViewModelById(targetId)!;
            sourceTeacherVM.Teacher = targetTeacher;
        }

        private void DeleteTeacher(Guid teacherId)
        {
            TeacherViewModel teacherVM = GetTeacherViewModelById(teacherId)!;
            _teacherVMs.Remove(teacherVM);
        }

        private void UnfocusTeacher()
        {
            SelectedTeacher = null;
        }

        private void TeacherStore_TeachersLoaded()
        {
            _teacherVMs.Clear();
            _teacherStore.Teachers.ToList().ForEach(
                t =>
                {
                    Teacher clonedTeacher = SerializationCopier.DeepCopy(t)!;
                    clonedTeacher.Groups = [];
                    AddTeacher(clonedTeacher);
                });
        }

        private void TeacherStore_TeacherAdded(Teacher teacher)
        {
            RefreshDependenciesOnAdding(teacher);
            AddTeacher(teacher);
            OnPropertyChanged(nameof(TeacherVMs));
        }

        private void TeacherStore_TeacherUpdated(Teacher targetTeacher)
        {
            RefreshDependenciesOnUpdating(targetTeacher);
            UpdateTeacher(targetTeacher);
            OnPropertyChanged(nameof(TeacherVMs));
        }

        private void TeacherStore_TeacherDeleted(Guid teacherId)
        {
            RefreshDependenciesOnDeleting(teacherId);
            DeleteTeacher(teacherId);
            OnPropertyChanged(nameof(TeacherVMs));
        }

        private void ViewStore_TeacherUnfocused()
        {
            UnfocusTeacher();
        }
    }
}
