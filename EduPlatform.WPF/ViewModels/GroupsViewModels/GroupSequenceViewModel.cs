using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Commands.GroupCommands;
using EduPlatform.Domain.Models;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.CoursesViewModels;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using EduPlatform.WPF.Service;
using EduPlatform.WPF.ViewModels.TeachersViewModels;

namespace EduPlatform.WPF.ViewModels.GroupsViewModels
{
    public class GroupSequenceViewModel : ViewModelBase, ISequenceViewModel
    {
        private readonly ObservableCollection<GroupViewModel> _groupVMs;

        public IEnumerable<GroupViewModel> GroupVMs =>
            _groupVMs.Select(gvm => new GroupViewModel(gvm.Group, _groupStore, _studentStore));

        public GroupViewModel? SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                OnPropertyChanged(nameof(SelectedGroup));

                if (UpdateGroupCommand is not null)
                {
                    ((OpenUpdateGroupFormCommand)UpdateGroupCommand).UpdatingGroup = value;
                    ((CommandBase)UpdateGroupCommand).OnCanExecutedChanged();
                }

                if (DeleteGroupCommand is not null)
                {
                    ((DeleteGroupCommand)DeleteGroupCommand).DeletingGroup = value;
                    ((CommandBase)DeleteGroupCommand).OnCanExecutedChanged();
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

        public ICommand? LoadGroupsCommand { get; private set; }
        public ICommand? CreateGroupCommand { get; private set; }
        public ICommand? UpdateGroupCommand { get; private set; }
        public ICommand? DeleteGroupCommand { get; private set; }

        private readonly GroupStore _groupStore;
        private readonly StudentStore _studentStore;
        private readonly ViewStore _viewStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private GroupViewModel? _selectedGroup;
        private CourseSequenceViewModel? _courseSequenceVM;
        private TeacherSequenceViewModel? _teacherSequenceVM;
        private StudentSequenceViewModel? _studentSequenceVM;
        private string? _errorMessage;


        public GroupSequenceViewModel(GroupStore groupStore,
            StudentStore studentStore,
            ViewStore viewStore,
            ModalNavigationStore modalNavigationStore)
        {
            _groupVMs = [];

            _groupStore = groupStore;
            _groupStore.GroupsLoaded += GroupsStore_GroupsLoaded;
            _groupStore.GroupAdded += GroupStore_GroupAdded;
            _groupStore.GroupUpdated += GroupStore_GroupUpdated;
            _groupStore.GroupDeleted += GroupStore_GroupDeleted;

            _studentStore = studentStore;

            _viewStore = viewStore;
            _viewStore.GroupUnfocused += ViewStore_GroupUnfocused;

            _modalNavigationStore = modalNavigationStore;
        }

        public override void Dispose()
        {
            _groupStore.GroupsLoaded -= GroupsStore_GroupsLoaded;
            _groupStore.GroupAdded -= GroupStore_GroupAdded;
            _groupStore.GroupUpdated -= GroupStore_GroupUpdated;
            _groupStore.GroupDeleted -= GroupStore_GroupDeleted;
            _viewStore.GroupUnfocused -= ViewStore_GroupUnfocused;

            base.Dispose();
        }

        public void SetCourseSequence(CourseSequenceViewModel newCourseSequence)
        {
            _courseSequenceVM = newCourseSequence;
        }

        public void SetTeacherSequence(TeacherSequenceViewModel newTeacherSequence)
        {
            _teacherSequenceVM = newTeacherSequence;
        }

        public void SetStudentSequence(StudentSequenceViewModel newStudentSequence)
        {
            _studentSequenceVM = newStudentSequence;
        }

        public void ConfigureCommands()
        {
            LoadGroupsCommand = new LoadGroupsCommand(_groupStore, this);

            CreateGroupCommand = new OpenCreateGroupFormCommand
            (
                _groupStore,
                _viewStore,
                _modalNavigationStore,
                _courseSequenceVM,
                _teacherSequenceVM,
                _studentSequenceVM
            );

            UpdateGroupCommand = new OpenUpdateGroupFormCommand
            (
                _groupStore,
                _viewStore,
                _modalNavigationStore,
                _courseSequenceVM,
                _teacherSequenceVM,
                _studentSequenceVM
            );

            DeleteGroupCommand = new DeleteGroupCommand(_groupStore);
        }

        public Task? LoadGroups()
        {
            return (LoadGroupsCommand as LoadGroupsCommand)?.ExecuteAsync(null);
        }

        private void AddGroup(Group groupItem)
        {
            GroupViewModel item = new(groupItem, _groupStore, _studentStore);
            _groupVMs.Add(item);
        }

        private void UpdateGroup(Group targetGroup)
        {
            GroupViewModel? sourceGroupVM = GetGroupViewModelById(targetGroup.GroupId);

            if (sourceGroupVM is null)
            {
                return;
            }

            sourceGroupVM.Group = targetGroup;
        }

        private void DeleteGroup(Guid groupId)
        {
            GroupViewModel? group = GetGroupViewModelById(groupId);

            if (group is null)
            {
                return;
            }

            _groupVMs.Remove(group);
        }

        private void UnfocusGroup()
        {
            SelectedGroup = null;
        }

        private GroupViewModel? GetGroupViewModelById(Guid groupId)
        {
            GroupViewModel? groupVM = _groupVMs.FirstOrDefault(g => g.GroupId == groupId);
            return groupVM;
        }

        private void RefreshDependenciesOnAdding(Group newGroup)
        {
            newGroup.Course?.Groups.Add(newGroup);

            newGroup.Students
                .ToList()
                .ForEach(
                    target =>
                    {
                        target.Group = newGroup;
                        target.GroupId = newGroup.GroupId;
                    });

            newGroup.Teachers
                .ToList()
                .ForEach(target => { target.Groups.Add(newGroup); });
        }

        private void RefreshDependenciesOnUpdating(Group targetGroup)
        {
            GroupViewModel sourceGroup = GetGroupViewModelById(targetGroup.GroupId)!;

            sourceGroup.Group.Course?.Groups.Remove(sourceGroup.Group);

            sourceGroup.StudentVMs
                .ToList()
                .ForEach(
                    svm =>
                    {
                        svm.Student.Group = null;
                        svm.Student.GroupId = null;
                    });

            sourceGroup.TeacherVMs
                .ToList()
                .ForEach(tvm => { tvm.Teacher.Groups.Remove(sourceGroup.Group); });


            targetGroup.Course?.Groups.Add(targetGroup);

            targetGroup.Students
                .ToList()
                .ForEach(
                    target =>
                    {
                        target.Group = targetGroup;
                        target.GroupId = targetGroup.GroupId;
                    });

            targetGroup.Teachers
                .ToList()
                .ForEach(target => { target.Groups.Add(targetGroup); });
        }

        private void RefreshDependenciesOnDeleting(Guid groupId)
        {
            GroupViewModel groupVM = GetGroupViewModelById(groupId)!;

            groupVM.Group.Course?.Groups.Remove(groupVM.Group);
            groupVM.Group.Teachers.ToList().ForEach(t => t.Groups.Remove(groupVM.Group));
            groupVM.Group.Students.ToList().ForEach(
                s =>
                {
                    s.GroupId = null;
                    s.Group = null;
                });
        }

        private void GroupsStore_GroupsLoaded()
        {
            _groupVMs.Clear();
            _groupStore.Groups.ToList().ForEach(
                g =>
                {
                    Group clonedGroup = SerializationCopier.DeepCopy(g)!;
                    clonedGroup.Course = null;
                    clonedGroup.Teachers = [];
                    clonedGroup.Students = [];
                    AddGroup(clonedGroup);
                });
        }

        private void GroupStore_GroupAdded(Group newGroup)
        {
            RefreshDependenciesOnAdding(newGroup);
            AddGroup(newGroup);
            OnPropertyChanged(nameof(GroupVMs));
        }

        private void GroupStore_GroupUpdated(Group targetGroup)
        {
            RefreshDependenciesOnUpdating(targetGroup);
            UpdateGroup(targetGroup);
            OnPropertyChanged(nameof(GroupVMs));
        }

        private void GroupStore_GroupDeleted(Guid groupId)
        {
            RefreshDependenciesOnDeleting(groupId);
            DeleteGroup(groupId);
            OnPropertyChanged(nameof(GroupVMs));
        }

        private void ViewStore_GroupUnfocused()
        {
            UnfocusGroup();
        }
    }
}