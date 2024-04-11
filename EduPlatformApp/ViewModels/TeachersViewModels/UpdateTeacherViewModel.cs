using EduPlatform.WPF.Commands.GeneralCommands;
using EduPlatform.WPF.Commands.StudentCommands;
using EduPlatform.WPF.Commands.TeacherCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GeneralViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using System.Windows.Input;

namespace EduPlatform.WPF.ViewModels.TeachersViewModels
{
    public class UpdateTeacherViewModel : ViewModelBase
    {
        public TeacherDetailsFormViewModel TeacherDetailsFormVM { get; }

        public UpdateTeacherViewModel(TeacherStore teacherStore,
                                      TeacherViewModel selectedTeacher,
                                      ViewStore viewStore,
                                      ModalNavigationStore modalNavigationStore,
                                      GroupSequenceViewModel groupSequenceVM)
        {
            //ICommand submitCommand = new SubmitUpdateStudentCommand(selectedTeacher, modalNavigationStore, teacherStore);
            //ICommand cancelCommand = new CloseFormCommand(modalNavigationStore);

            ICommand submitCommand = new SubmitUpdateTeacherCommand(selectedTeacher, modalNavigationStore, teacherStore);
            ICommand cancelCommand = new CloseFormCommand(modalNavigationStore);

            TeacherDetailsFormVM = new(groupSequenceVM, submitCommand, cancelCommand)
            {
                FirstName = selectedTeacher.FirstName,
                LastName = selectedTeacher.LastName,
            };

            TeacherDetailsFormVM.GroupVMs
                .Where(gvm => selectedTeacher.Groups.Any(g => g.GroupId == gvm.GroupId))
                .ToList()
                .ForEach(gvm => gvm.IsChecked = true);

            ((SubmitUpdateTeacherCommand)submitCommand).FormDetails = TeacherDetailsFormVM;
            viewStore.UnfocuseTeacher();
        }
    }
}
