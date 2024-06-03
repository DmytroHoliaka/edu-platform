namespace EduPlatform.WPF.Commands.BaseCommands
{
    public abstract class AsyncCommandBase : CommandBase
    {
        public override async void Execute(object? parameter)
        {
            try
            {
                await ExecuteAsync(parameter);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public abstract Task ExecuteAsync(object? parameter);
    }
}
