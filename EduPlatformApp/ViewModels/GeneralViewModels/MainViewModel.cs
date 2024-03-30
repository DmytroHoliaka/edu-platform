using EduPlatform.WPF.Service;
using EduPlatform.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduPlatform.WPF.ViewModels.GeneralViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public EduPlatformViewModel EduPlatformViewModel { get; }
        private ModalNavigationStore _modalNavigationStore { get; }

        public MainViewModel(EduPlatformViewModel eduPlatformViewModel, ModalNavigationStore modalNavigationStore)
        {
            EduPlatformViewModel = eduPlatformViewModel;
            _modalNavigationStore = modalNavigationStore;
        }
    }
}
