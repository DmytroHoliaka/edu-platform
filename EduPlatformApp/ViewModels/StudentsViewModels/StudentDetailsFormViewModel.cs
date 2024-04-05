using EduPlatform.WPF.Commands;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.TeachersViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EduPlatform.WPF.ViewModels.StudentsViewModels
{
    public class StudentDetailsFormViewModel : ViewModelBase
    {
        public ObservableCollection<GroupViewModel> GroupVMs { get; }

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
                (SubmitCommand as SubmitCreateStudentCommand)?.OnCanExecutedChanded();
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
                (SubmitCommand as SubmitCreateStudentCommand)?.OnCanExecutedChanded();
            }
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        private string? _firstName;
        private string? _lastName;


        public StudentDetailsFormViewModel
        (
            Guid guid,
            GroupSequenceViewModel groupSequenceVM,
            ICommand submitCommand,
            ICommand cancelCommand
        )
        {
            GroupVMs = new(groupSequenceVM.GroupVMs);
            SetupEvents();

            SubmitCommand = submitCommand;
            CancelCommand = cancelCommand;
        }

        protected override void Dispose()
        {
            foreach (GroupViewModel group in GroupVMs)
            {
                group.PropertyChanged -= Group_PropertyChanged;
            }

            base.Dispose();
        }

        private void SetupEvents()
        {
            foreach (GroupViewModel group in GroupVMs)
            {
                group.PropertyChanged += Group_PropertyChanged;
            }
        }

        private void Group_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(GroupViewModel.IsChecked))
            {
                (SubmitCommand as SubmitCreateStudentCommand)?.OnCanExecutedChanded();
            }
        }
    }
}
