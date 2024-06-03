using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EduPlatform.WPF.ViewModels.StudentsViewModels
{
    public class StudentDetailsFormViewModel : ViewModelBase
    {
        public ObservableCollection<GroupViewModel> GroupVMs { get; }

        public string? FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        public string? LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }
        
        public string? ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(HasErrorMessage));
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public bool CanSubmit =>
            string.IsNullOrWhiteSpace(FirstName) == false
            && string.IsNullOrWhiteSpace(LastName) == false;
        
        public bool HasErrorMessage => string.IsNullOrEmpty(_errorMessage) == false;

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        private string? _firstName;
        private string? _lastName;
        private string? _errorMessage;

        public StudentDetailsFormViewModel(
            GroupSequenceViewModel groupSequenceVM,
            ICommand submitCommand,
            ICommand cancelCommand
        )
        {
            GroupVMs = new ObservableCollection<GroupViewModel>(groupSequenceVM.GroupVMs);
            SetMarkers();

            SubmitCommand = submitCommand;
            CancelCommand = cancelCommand;
        }

        private void SetMarkers()
        {
            GroupVMs.ToList().ForEach(gvm => gvm.IsEnabled = true);
        }
    }
}
