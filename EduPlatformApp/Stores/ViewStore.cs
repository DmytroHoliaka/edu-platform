namespace EduPlatform.WPF.Stores
{
    public class ViewStore
    {
        public event Action? GroupUnfocused;
        public event Action? StudentUnfocused;

        public void UnfocuseGroup()
        {
            GroupUnfocused?.Invoke();
        }

        public void UnfocuseStudent()
        {
            StudentUnfocused?.Invoke();
        }
    }
}
