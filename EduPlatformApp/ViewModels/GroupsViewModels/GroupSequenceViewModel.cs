using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Commands.GroupCommands;
using EduPlatform.Domain.Models;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.CoursesViewModels;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.TeachersViewModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EduPlatform.WPF.ViewModels.StudentsViewModels;

namespace EduPlatform.WPF.ViewModels.GroupsViewModels
{
    public class GroupSequenceViewModel : ViewModelBase
    {
        private readonly ObservableCollection<GroupViewModel> _groupVMs;
        public IEnumerable<GroupViewModel> GroupVMs =>
            _groupVMs.Select(gvm => new GroupViewModel(gvm.Group));

        public GroupViewModel? SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                OnPropertyChanged(nameof(SelectedGroup));

                ((OpenUpdateGroupFormCommand)UpdateGroupCommand).UpdatingGroup = value;
                ((CommandBase)UpdateGroupCommand).OnCanExecutedChanged();

                ((DeleteGroupCommand)DeleteGroupCommand).DeletingGroup = value;
                ((CommandBase)DeleteGroupCommand).OnCanExecutedChanged();
            }
        }

        public ICommand LoadGroupsCommand { get; private set; }
        public ICommand CreateGroupCommand { get; private set; }
        public ICommand UpdateGroupCommand { get; private set; }
        public ICommand DeleteGroupCommand { get; private set; }

        private readonly GroupStore _groupStore;
        private readonly ViewStore _viewStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private GroupViewModel? _selectedGroup;
        private CourseSequenceViewModel? _courseSequenceVM;
        private TeacherSequenceViewModel? _teacherSequenceVM;
        private StudentSequenceViewModel? _studentSequenceVM;


        public GroupSequenceViewModel(GroupStore groupStore,
                                      ViewStore viewStore,
                                      ModalNavigationStore modalNavigationStore)
        {
            _groupVMs = [];

            _groupStore = groupStore;
            _groupStore.GroupsLoaded += GroupsStore_GroupsLoaded;
            _groupStore.GroupAdded += GroupStore_GroupAdded;
            _groupStore.GroupUpdated += GroupStore_GroupUpdated;
            _groupStore.GroupDeleted += GroupStore_GroupDeleted;

            _viewStore = viewStore;
            _viewStore.GroupUnfocused += ViewStore_GroupUnfocused;

            _modalNavigationStore = modalNavigationStore;
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
            LoadGroupsCommand = new LoadGroupsCommand(_groupStore);

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

        // ToDo: Remove
        public void InsertTestData()
        {
            _groupVMs.Add(new GroupViewModel(new Group()
            {
                GroupId = Guid.NewGuid(),
                Name = "Group 1",
                CourseId = Guid.Empty,
            }));

            _groupVMs.Add(new GroupViewModel(new Group()
            {
                GroupId = Guid.NewGuid(),
                Name = "Group 2",
                CourseId = Guid.Empty,
            }));

            _groupVMs.Add(new GroupViewModel(new Group()
            {
                GroupId = Guid.NewGuid(),
                Name = "Group 3",
                CourseId = Guid.Empty,
            }));

            _groupVMs.Add(new GroupViewModel(new Group()
            {
                GroupId = Guid.NewGuid(),
                Name = "Group 4",
                CourseId = Guid.Empty,
            }));


        }

        protected override void Dispose()
        {
            _groupStore.GroupAdded -= GroupStore_GroupAdded;
            _groupStore.GroupUpdated -= GroupStore_GroupUpdated;

            base.Dispose();
        }

        public void LoadGroups()
        {
            LoadGroupsCommand.Execute(null);
        }

        private void AddGroup(Group groupItem)
        {
            GroupViewModel item = new(groupItem);
            _groupVMs.Add(item);
        }

        private void UpdateGroup(Group targetGroup)
        {
            GroupViewModel sourceGroupVM = GetGroupViewModelById(targetGroup.GroupId)!;

            if (sourceGroupVM is null)
            {
                return;
            }

            sourceGroupVM.Group = targetGroup;
        }

        private void DeleteGroup(Guid groupId)
        {
            GroupViewModel group = GetGroupViewModelById(groupId)!;

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
               ?.ToList()
                .ForEach(target =>
                {
                    target.Group = newGroup;
                    target.GroupId = newGroup.GroupId;
                });

            newGroup.Teachers
               ?.ToList()
                .ForEach(target =>
                {
                    target.Groups.Add(newGroup);
                });
        }

        private void RefreshDependenciesOnUpdating(Group targetGroup)
        {
            GroupViewModel sourceGroup = GetGroupViewModelById(targetGroup.GroupId)!;

            sourceGroup.Group.Course?.Groups.Remove(sourceGroup.Group);

            sourceGroup.StudentVMs
                .ToList()
                .ForEach(svm =>
                {
                    svm.Student.Group = null;
                    svm.Student.GroupId = null;
                });

            sourceGroup.TeacherVMs
                .ToList()
                .ForEach(tvm =>
                {
                    tvm.Teacher.Groups.Remove(sourceGroup.Group);
                });


            targetGroup.Course?.Groups.Add(targetGroup);
            
            targetGroup.Students
               ?.ToList()
                .ForEach(target =>
                {
                    target.Group = targetGroup;
                    target.GroupId = targetGroup.GroupId;
                });

            targetGroup.Teachers
               ?.ToList()
                .ForEach(target =>
                {
                    target.Groups.Add(targetGroup);
                });
        }

        private void RefreshDependenciesOnDeleting(Guid groupId)
        {
            GroupViewModel groupVM = GetGroupViewModelById(groupId)!;

            groupVM.Group.Course?.Groups.Remove(groupVM.Group);
            groupVM.Group.Teachers.ToList().ForEach(t => t.Groups.Remove(groupVM.Group));
            groupVM.Group.Students.ToList().ForEach(s =>
            {
                s.GroupId = null;
                s.Group = null;
            });
        }

        private void GroupsStore_GroupsLoaded()
        {
            _groupVMs.Clear();
            _groupStore.Groups.ToList().ForEach(AddGroup);
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
