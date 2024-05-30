namespace EduPlatform.WPF.Service.Time;

public class SystemClock : IClock
{
    public DateTime Now => DateTime.Now;
}