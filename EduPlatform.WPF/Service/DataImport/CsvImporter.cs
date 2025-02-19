﻿using System.IO;
using System.Text;
using System.Windows.Forms;
using EduPlatform.Domain.Models;
using EduPlatform.WPF.Service.Utilities;
using EduPlatform.WPF.Service.Validators;
using EduPlatform.WPF.Stores;

namespace EduPlatform.WPF.Service.DataImport;

public class CsvImporter : IDataImporter
{
    public async Task ImportStudents(StudentStore? studentStore, Group? group, string? csvPath)
    {
        ArgumentNullException.ThrowIfNull(studentStore, nameof(studentStore));
        ArgumentNullException.ThrowIfNull(group, nameof(group));
        ArgumentException.ThrowIfNullOrWhiteSpace(csvPath);

        CsvValidator validator = new();
        List<RowError> errors = validator.GetFileErrors(csvPath).ToList();

        if (errors.Count > 0)
        {
            PrintErrors(errors);
            return;
        }

        using (StreamReader reader = new(csvPath))
        {
            await reader.ReadLineAsync();

            while (await reader.ReadLineAsync() is { } line)
            {
                string[] names = line.Split(',').Select(s => s.Trim()).ToArray();

                string firstName = names[1];
                string lastName = names[2];

                Student student = new()
                {
                    StudentId = Guid.NewGuid(),
                    FirstName = firstName,
                    LastName = lastName,
                    GroupId = group.GroupId,
                    Group = group
                };

                await studentStore.Add(student);
            }
        }

        MessageBox.Show("Import of students was successful");
    }

    private static void PrintErrors(List<RowError> errors)
    {
        const int hyphenCount = 18;
        StringBuilder complexLine = new();

        complexLine.Append("\t\t");
        complexLine.Append(new string('-', hyphenCount));
        complexLine.Append(" Errors ");
        complexLine.Append(new string('-', hyphenCount));
        complexLine.Append("\t\t");
        complexLine.Append(Environment.NewLine);
        complexLine.Append(Environment.NewLine);
        
        complexLine.Append(CollectionService.GetString(errors));
        MessageBox.Show(complexLine.ToString());
    }
}