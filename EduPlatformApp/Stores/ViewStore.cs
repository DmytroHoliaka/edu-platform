namespace EduPlatform.WPF.Stores
{
    public class ViewStore
    {
        public event Action? GroupUnfocused;
        public event Action? StudentUnfocused;
        public event Action? TeacherUnfocused;

        public void UnfocuseGroup()
        {
            GroupUnfocused?.Invoke();
        }

        public void UnfocuseStudent()
        {
            StudentUnfocused?.Invoke();
        }

        public void UnfocuseTeacher()
        {
            TeacherUnfocused?.Invoke();
        }
    }
}
