using EduPlatform.WPF.Commands;
using EduPlatform.WPF.Models;
using EduPlatform.WPF.Stores;
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
                _teacherSequenceVM, 
                _studentSequenceVM
            );

            UpdateGroupCommand = new OpenUpdateGroupFormCommand
            (
                _groupStore, 
                _viewStore, 
                _modalNavigationStore, 
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
                Teachers = _teacherSequenceVM.TeacherVMs.Select(tvm => tvm.Teacher).Take(2).ToList(),
                Students = _studentSequenceVM.StudentVMs.Select(svm => svm.Student).Take(2).ToList(),
            }));

            _groupVMs.Add(new GroupViewModel(new Group()
            {
                GroupId = Guid.NewGuid(),
                Name = "Group 2",
                CourseId = Guid.Empty,
                Teachers = _teacherSequenceVM.TeacherVMs.Select(tvm => tvm.Teacher).TakeLast(2).ToList(),
                Students = _studentSequenceVM.StudentVMs.Select(svm => svm.Student).TakeLast(2).ToList(),
            }));
        }

        public void AddGroup(Group? groupItem)
        {
            if (groupItem is null)
            {
                return;
            }

            GroupViewModel item = new(groupItem);

            _groupVMs.Add(item);
            OnPropertyChanged(nameof(GroupVMs));
        }

        public void UpdateGroup(Guid sourceId, Group? targetGroup)
        {
            if (targetGroup is null)
            {
                return;
            }

            GroupViewModel? sourceGroupVM = _groupVMs.FirstOrDefault(g => g.GroupId == targetGroup.GroupId);

            if (sourceGroupVM is null)
            {
                return;
            }

            sourceGroupVM.Group = targetGroup;
            OnPropertyChanged(nameof(GroupVMs));
        }

        public void DeleteGroup(Guid groupId)
        {
            GroupViewModel? group = _groupVMs.FirstOrDefault(g => g.GroupId == groupId);

            if (group is null)
            {
                return;
            }

            _groupVMs.Remove(group);
            OnPropertyChanged(nameof(GroupVMs));
        }

        public void UnfocusGroup()
        {
            SelectedGroup = null;
        }

        protected override void Dispose()
        {
            _groupStore.GroupAdded -= GroupStore_GroupAdded;
            _groupStore.GroupUpdated -= GroupStore_GroupUpdated;

            base.Dispose();
        }

        private void GroupStore_GroupAdded(Group groupItem)
        {
            AddGroup(groupItem);
        }

        private void GroupStore_GroupUpdated(Guid sourceId, Group targetGroup)
        {
            UpdateGroup(sourceId, targetGroup);
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
