using System.IO;

namespace EduPlatform.WPF.Service.Validators;

public static class GeneralFileValidator
{
    public static bool IsExtensionCorrect(string? path, string? extension)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(extension))
        {
            return false;
        }

        FileInfo fileInfo = new(path);
        return fileInfo.Extension == extension;
    }
}