using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EduPlatform.WPF.ViewModels.CoursesViewModels
{
    public class CourseDetailsFormViewModel : ViewModelBase
    {
        public ObservableCollection<GroupViewModel> GroupVMs { get; }

        public string? CourseName
        {
            get => _courseName;
            set
            {
                _courseName = value;
                OnPropertyChanged(nameof(CourseName));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        public string? Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
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
        
        public bool HasErrorMessage => string.IsNullOrEmpty(_errorMessage) == false;
        public bool CanSubmit => string.IsNullOrWhiteSpace(CourseName) == false;

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        private string? _courseName;
        private string? _description;
        private string? _errorMessage;

        public CourseDetailsFormViewModel
        (
            GroupSequenceViewModel groupSequenceVM,
            ICommand submitCommand,
            ICommand cancelCommand
        )
        {
            GroupVMs = new(groupSequenceVM.GroupVMs);
            SetMarkers();

            SubmitCommand = submitCommand;
            CancelCommand = cancelCommand;
        }

        private void SetMarkers()
        {
            GroupVMs.Where(gvm => gvm.CourseId is null).ToList().ForEach(gvm => gvm.IsEnabled = true);
        }
    }
}
