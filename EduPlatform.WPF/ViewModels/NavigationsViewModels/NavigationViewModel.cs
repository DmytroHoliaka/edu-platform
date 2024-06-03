using System.Windows.Input;
using EduPlatform.WPF.Commands.NavigationCommands;
using EduPlatform.WPF.Service.Utilities;
using EduPlatform.WPF.ViewModels.GeneralViewModels;

namespace EduPlatform.WPF.ViewModels.NavigationsViewModels
{
    public class NavigationViewModel : ViewModelBase
    {
        public PageId PageId
        {
            get => _pageId;
            set
            {
                _pageId = value;
                OnPropertyChanged(nameof(PageId));
            }
        }

        public ICommand ChangePageCommand { get; }
        private PageId _pageId;

        public NavigationViewModel()
        {
            PageId = PageId.Overview;
            ChangePageCommand = new ChangePageCommand(this);
        }
    }
}