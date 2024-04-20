using EduPlatform.WPF.Stores;

namespace EduPlatform.WPF.ViewModels.GeneralViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public HubViewModel EduPlatformViewModel { get; }
        private readonly ModalNavigationStore _modalNavigationStore;

        public ViewModelBase? CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
        public bool IsModalOpen => _modalNavigationStore.IsOpen;


        public MainViewModel(HubViewModel eduPlatformViewModel, ModalNavigationStore modalNavigationStore)
        {
            EduPlatformViewModel = eduPlatformViewModel;
            _modalNavigationStore = modalNavigationStore;

            _modalNavigationStore.CurrentViewModelChanged += ModalNavigationStore_CurrentViewModelChanged;
        } 

        protected override void Dispose()
        {
            _modalNavigationStore.CurrentViewModelChanged -= ModalNavigationStore_CurrentViewModelChanged;
            base.Dispose();
        }

        private void ModalNavigationStore_CurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModel));
            OnPropertyChanged(nameof(IsModalOpen));
        }
    }
}
