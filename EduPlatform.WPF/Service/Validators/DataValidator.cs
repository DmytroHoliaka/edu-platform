namespace EduPlatform.WPF.Service.Validators;

public abstract class DataValidator
{
    public static bool IsValidCorrect(string lineId)
    {
        if (int.TryParse(lineId, out int id) == false)
        {
            return false;
        }

        if (id < 0)
        {
            return false;
        }

        return true;
    }
}