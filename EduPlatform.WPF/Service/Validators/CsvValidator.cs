using System.IO;

namespace EduPlatform.WPF.Service.Validators;

public class CsvValidator : IFileValidator
{
    private const string FirstColumn = "LocalId";
    private const string SecondColumn = "FirstName";
    private const string ThirdColumn = "LastName";
    private const int ColumnCount = 3;
    private const string Extension = ".csv";

    public IEnumerable<RowError> GetFileErrors(string? path)
    {
        List<RowError> errors = [];

        if (string.IsNullOrWhiteSpace(path))
        {
            errors.Add(new RowError(lineNumber:-1,
                                    columnName:"<not specified>",
                                    explanation:"Incorrect file path."));
            return errors;
        }

        if (File.Exists(path) == false)
        {
            errors.Add(new RowError(lineNumber: -1,
                                    columnName: "<not specified>",
                                    explanation: "There is no file at the specified path"));
            return errors;
        }

        if (GeneralFileValidator.IsExtensionCorrect(path, Extension) == false)
        {
            errors.Add(new RowError(lineNumber: -1,
                                    columnName: "<not specified>",
                                    explanation: "Incorrect file extension"));
            return errors;
        }

        using (StreamReader reader = new(path))
        {
            string? headerLine = reader.ReadLine();
            AddHeaderErrors(
                headerLine: headerLine, 
                errors: errors);
            
            if (errors.Count != 0)
            {
                return errors;
            }

            AddBodyHeaders(
                reader: reader,
                errors: errors);
        }

        return errors;
    }

    private static void AddHeaderErrors(string? headerLine, List<RowError> errors)
    {
        if (headerLine is null)
        {
            errors.Add(new RowError(lineNumber: 1,
                                     columnName: "<not specified>",
                                     explanation: "The file is empty"));
            return;
        }

        string[] columns = headerLine.Split(",");

        if (columns.Length != ColumnCount)
        {
            errors.Add(new RowError(lineNumber: 1,
                                     columnName: "<not specified>",
                                     explanation: $"Incorrect amount of columns (must be {ColumnCount})"));
            return;
        }

        if (columns[0] != FirstColumn)
        {
            errors.Add(new RowError(lineNumber: 1,
                                     columnName: FirstColumn,
                                     explanation: $"Incorrect name of first column (must be {FirstColumn})"));
        }

        if (columns[1] != SecondColumn)
        {
            errors.Add(new RowError(lineNumber: 1,
                                     columnName: SecondColumn,
                                     explanation: $"Incorrect name of second column (must be {SecondColumn})"));
        }

        if (columns[2] != ThirdColumn)
        {
            errors.Add(new RowError(lineNumber: 1,
                                     columnName: ThirdColumn,
                                     explanation: $"Incorrect name of third column (must be {ThirdColumn})"));
        }
    }

    private static void AddBodyHeaders(StreamReader reader, List<RowError> errors)
    {
        int lineCount = 1;
        List<int> ids = [];

        while (reader.ReadLine() is { } line)
        {
            lineCount += 1;
            string[] columns = line.Split(',');

            if (columns.Length != ColumnCount)
            {
                errors.Add(new RowError(lineNumber: lineCount,
                                        columnName: $"<not specified>",
                                        explanation: "Incorrect number of columns"));

                continue;
            }

            string localId = columns[0];
            string firstName = columns[1];
            string lastName = columns[2];

            if (DataValidator.IsValidCorrect(localId) == false)
            {
                errors.Add(new RowError(lineNumber: lineCount,
                                        columnName: FirstColumn,
                                        explanation: "Incorrect identifier value"));
            }
            else if (ids.Contains(Convert.ToInt32(localId)))
            {
                errors.Add(new RowError(lineNumber: lineCount,
                                        columnName: FirstColumn,
                                        explanation: "The value of the identifier is not unique"));
            }
            else
            {
                ids.Add(Convert.ToInt32(localId));
            }

            if (string.IsNullOrWhiteSpace(firstName))
            {
                errors.Add(new RowError(lineNumber: lineCount,
                                        columnName: SecondColumn,
                                        explanation: "Cannot be empty or white space"));
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                errors.Add(new RowError(lineNumber: lineCount,
                                        columnName: ThirdColumn,
                                        explanation: "Cannot be empty or white space"));
            }
        }
    }
}