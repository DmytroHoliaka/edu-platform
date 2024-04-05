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
        public ObservableCollection<GroupViewModel> GroupVMs { get; }

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

        public ICommand CreateGroupCommand { get; }
        public ICommand UpdateGroupCommand { get; }
        public ICommand DeleteGroupCommand { get; }

        private GroupViewModel? _selectedGroup;
        private readonly GroupStore _groupStore;
        private readonly ViewStore _viewStore;

        public GroupSequenceViewModel(GroupStore groupStore,
                                      ViewStore viewStore,
                                      ModalNavigationStore modalNavigationStore,
                                      TeacherSequenceViewModel teacherSequenceVM,
                                      StudentSequenceViewModel studentSequenceVM)
        {
            GroupVMs = [];

            _groupStore = groupStore;
            _groupStore.GroupAdded += GroupStore_GroupAdded;
            _groupStore.GroupUpdated += GroupStore_GroupUpdated;
            _groupStore.GroupDeleted += GroupStore_GroupDeleted;

            _viewStore = viewStore;
            _viewStore.GroupUnfocused += ViewStore_GroupUnfocused;

            CreateGroupCommand = new OpenCreateGroupFormCommand(_groupStore, _viewStore, modalNavigationStore, teacherSequenceVM, studentSequenceVM);
            UpdateGroupCommand = new OpenUpdateGroupFormCommand(_groupStore, _viewStore, modalNavigationStore, teacherSequenceVM, studentSequenceVM);
            DeleteGroupCommand = new DeleteGroupCommand(_groupStore);
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

        private void AddGroup(Group groupItem)
        {
            GroupViewModel item = new(groupItem);

            GroupVMs.Add(item);
        }

        private void GroupStore_GroupUpdated(Guid sourceId, Group targetGroup)
        {
            UpdateGroup(sourceId, targetGroup);
        }

        private void UpdateGroup(Guid sourceId, Group targetGroup)
        {
            GroupViewModel? sourceGroupVM = GroupVMs.FirstOrDefault(g => g.GroupId == targetGroup.GroupId);

            if (sourceGroupVM is null)
            {
                return;
            }

            sourceGroupVM.Group = targetGroup;
        }

        private void GroupStore_GroupDeleted(Guid groupId)
        {
            DeleteGroup(groupId);
        }

        private void DeleteGroup(Guid groupId)
        {
            GroupViewModel? group = GroupVMs.FirstOrDefault(g => g.GroupId == groupId);

            if (group is null)
            {
                return;
            }

            GroupVMs.Remove(group);
        }

        private void ViewStore_GroupUnfocused()
        {
            UnfocusGroup();
        }

        private void UnfocusGroup()
        {
            SelectedGroup = null;
        }
    }
}
