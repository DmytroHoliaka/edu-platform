using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EduPlatform.WPF.ViewModels.StudentsViewModels
{
    public class StudentDetailsFormViewModel : ViewModelBase
    {
        public ObservableCollection<GroupViewModel> GroupVMs { get; }
        public Guid GroupId { get; }

        public string? FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string? LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        // ToDo: Try implement using CanExecute in Command
        //public bool CanSubmit
        //    => string.IsNullOrWhiteSpace(FirstName) == false &&
        //       string.IsNullOrWhiteSpace(LastName) == false;

        public ICommand? SubmitCommand { get; }
        public ICommand? CancelCommand { get; }

        private string? _firstName;
        private string? _lastName;


        public StudentDetailsFormViewModel
        (
            Guid id, 
            GroupSequenceViewModel groupSequenceVM,
            ICommand submitCommand, 
            ICommand cancelCommand
        )
        {
            GroupId = id;
            GroupVMs = new(groupSequenceVM.GroupVMs);
            SubmitCommand = submitCommand;
            CancelCommand = cancelCommand;
        }
    }
}
