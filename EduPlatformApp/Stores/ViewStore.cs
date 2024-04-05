namespace EduPlatform.WPF.Stores
{
    public class ViewStore
    {
        public event Action? GroupUnfocused;

        public void UnfocuseGroup()
        {
            GroupUnfocused?.Invoke();
        }
    }
}
