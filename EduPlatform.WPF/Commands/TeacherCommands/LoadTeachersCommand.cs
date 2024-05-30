using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using EduPlatform.WPF.ViewModels.TeachersViewModel;

namespace EduPlatform.WPF.Commands.TeacherCommands
{
    public class LoadTeachersCommand(TeacherStore teacherStore, TeacherSequenceViewModel teacherSequenceViewModel) : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                await teacherStore.Load();
            }
            catch (Exception)
            {
                teacherSequenceViewModel.ErrorMessage = "Unable to load student data from database";
            }
        }
    }
}
