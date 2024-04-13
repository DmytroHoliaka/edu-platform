using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace EduPlatform.WPF.ViewModels.TeachersViewModels
{
    public class CourseDetailsFormViewModel : ViewModelBase
    {
        public ObservableCollection<GroupViewModel> GroupVMs { get; }

        public string? CourseName
        {
            get
            {
                return _courseName;
            }
            set
            {
                _courseName = value;
                OnPropertyChanged(nameof(CourseName));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        public bool CanSubmit =>
            string.IsNullOrWhiteSpace(CourseName) == false;

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        private string? _courseName;


        public CourseDetailsFormViewModel
        (
            GroupSequenceViewModel groupSequenceVM,
            ICommand submitCommand,
            ICommand cancelCommand
        )
        {
            GroupVMs = new(groupSequenceVM.GroupVMs);

            SubmitCommand = submitCommand;
            CancelCommand = cancelCommand;
        }
    }
}
