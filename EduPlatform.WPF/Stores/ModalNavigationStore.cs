using EduPlatform.WPF.ViewModels.GeneralViewModels;

namespace EduPlatform.WPF.Stores
{
    public class ModalNavigationStore
    {
        public ViewModelBase? CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                CurrentViewModelChanged?.Invoke();
            }
        }

        public bool IsOpen => 
            _currentViewModel is not null;
        
        public event Action? CurrentViewModelChanged;

        private ViewModelBase? _currentViewModel;

        public void Close()
        {
            CurrentViewModel = null;
        }
    }
}
