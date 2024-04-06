using System.Windows.Input;

namespace EduPlatform.WPF.Commands
{
    public abstract class CommandBase : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public virtual bool CanExecute(object? parameter)
        {
            return true;
        }

        public abstract void Execute(object? parameter);

        public virtual void OnCanExecutedChanded()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
