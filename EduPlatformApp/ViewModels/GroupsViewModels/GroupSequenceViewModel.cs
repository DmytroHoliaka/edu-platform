using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Commands.GroupCommands;
using EduPlatform.WPF.Models;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.CoursesViewModels;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModel;
using EduPlatform.WPF.ViewModels.TeachersViewModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

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
                ((CommandBase)UpdateGroupCommand).OnCanExecutedChanded();

                ((DeleteGroupCommand)DeleteGroupCommand).DeletingGroup = value;
                ((CommandBase)DeleteGroupCommand).OnCanExecutedChanded();
            }
        }

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
            OnPropertyChanged(nameof(GroupVMs));
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

        private void RefreshDependentStudents(Group targetGroup)
        {
            GroupViewModel? sourceGroup = GetGroupViewModelById(targetGroup.GroupId);

            sourceGroup?.GroupStudents
                .ToList()
                .ForEach(svm =>
                {
                    svm.Student.Group = null;
                    svm.Student.GroupId = null;
                });

            targetGroup.Students
               ?.ToList()
                .ForEach(target =>
                {
                    target.Group = targetGroup;
                    target.GroupId = targetGroup.GroupId;
                });
        }

        private void RefreshDependentTeachers(Group targetGroup)
        {
            GroupViewModel? sourceGroup = GetGroupViewModelById(targetGroup.GroupId);

            sourceGroup?.GroupTeachers
                .ToList()
                .ForEach(tvm =>
                {
                    tvm.Teacher.Groups.Remove(sourceGroup.Group);
                });

            targetGroup.Teachers
               ?.ToList()
                .ForEach(target =>
                {
                    target.Groups.Add(targetGroup);
                });
        }

        private void GroupStore_GroupAdded(Group newGroup)
        {
            RefreshDependentStudents(newGroup);
            RefreshDependentTeachers(newGroup);
            AddGroup(newGroup);
            OnPropertyChanged(nameof(GroupVMs));
        }

        private void GroupStore_GroupUpdated(Group targetGroup)
        {
            RefreshDependentStudents(targetGroup);
            RefreshDependentTeachers(targetGroup);
            UpdateGroup(targetGroup);
            OnPropertyChanged(nameof(GroupVMs));
        }

        private void GroupStore_GroupDeleted(Guid groupId)
        {
            DeleteGroup(groupId);
        }

        private void ViewStore_GroupUnfocused()
        {
            UnfocusGroup();
        }
    }
}
