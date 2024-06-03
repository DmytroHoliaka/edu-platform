using System.Text;

namespace EduPlatform.WPF.Service.Utilities;

public static class CollectionService
{
    public static string GetString<T>(List<T>? list)
    {
        ArgumentNullException.ThrowIfNull(list, nameof(list));

        StringBuilder complexLine = new();

        foreach (T item in list)
        {
            complexLine.Append(item);
            complexLine.Append(Environment.NewLine);
        }

        return complexLine.ToString();
    }
}