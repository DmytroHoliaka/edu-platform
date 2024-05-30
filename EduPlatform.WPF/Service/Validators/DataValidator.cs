namespace EduPlatform.WPF.Service.Validators;

public abstract class DataValidator
{
    public static bool IsValidId(string? lineId)
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