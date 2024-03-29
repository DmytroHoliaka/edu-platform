using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EduPlatform.WPF.Service;
using System.Windows;
using System.Windows.Input;

namespace EduPlatform.WPF.ViewModels.NavigationsViewModel
{
    // ToDo: ObservableObject implements IPropertyChanged. Try add ObservableObject in ViewModelBase
    public class NavigationViewModel : ObservableObject
    {
        public PageId PageId
        {
            get => _pageId; 
            set => SetProperty(ref _pageId, value);
        }

        public ICommand ChangePageCommand => new RelayCommand<PageId>(ChangePage);
        
        private PageId _pageId;


        public NavigationViewModel()
        {
            PageId = PageId.Overview;
        }

        private void ChangePage(PageId newPage)
        {
            PageId = newPage;
        }
    }
}
