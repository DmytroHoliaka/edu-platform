using CommunityToolkit.Mvvm.Input;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EduPlatform.WPF.ViewModels.GroupViewModels
{
    public class EduPlatformGroupsViewModel : ViewModelBase
    {
        private readonly ObservableCollection<GroupListingItemViewModel> _groupListingItemViewModels;

        public IEnumerable<GroupListingItemViewModel>? GroupListingItemViewModels
            => _groupListingItemViewModels;

        public ICommand CreateGroupCommand => new RelayCommand(CreateGroup);
        public ICommand UpdateGroupCommand => new RelayCommand(UpdateGroup);
        public ICommand DeleteGroupCommand => new RelayCommand(DeleteGroup);

        public EduPlatformGroupsViewModel()
        {
            _groupListingItemViewModels =
            [
                new("Dmytro"),
                new("Alex"),
                new("Top"),
            ];
        }

        private void CreateGroup()
        {
            //MessageBox.Show("Create group");
            CreateGroupView view = new();
        }

        private void UpdateGroup()
        {
            MessageBox.Show("Update group");
        }

        private void DeleteGroup()
        {
            MessageBox.Show("Delete group");
        }
    }
}
