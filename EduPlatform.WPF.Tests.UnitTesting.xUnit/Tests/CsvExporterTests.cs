using EduPlatform.Domain.Models;
using EduPlatform.WPF.Service.DataExport;
using EduPlatform.WPF.Tests.UnitTesting.xUnit.Fixtures;
using EduPlatform.WPF.Tests.UnitTesting.xUnit.Utilities;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using Xunit;

namespace EduPlatform.WPF.Tests.UnitTesting.xUnit.Tests;

public class CsvExporterTests(CsvExporterFixture fixture) : IClassFixture<CsvExporterFixture>
{
    [Fact]
    public async Task ExportStudent_NullInput_ThrowsArgumentNullException()
    {
        // Arrange
        GroupViewModel? groupVM = null;
        CsvExporter exporter = new(clock: fixture.Mock.Object, folderPath: fixture.DirPath);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await exporter.ExportStudent(groupVM: groupVM));
    }

    [Fact]
    public async Task ExportStudent_GroupViewModelPlug_CreatesEmptyCsvFile()
    {
        // Arrange
        Group group = ModelGenerator.GetPlugGroup(groupName: "CsvExporterGroupTest");

        GroupViewModel groupVM = new(group);
        CsvExporter exporter = new(clock: fixture.Mock.Object, folderPath: fixture.DirPath);
        
        const string fileName = "Students of CsvExporterGroupTest(2023.08.14 06-30-15.7500).csv";
        string filePath = fixture.DirPath + "/" + fileName;

        // Act
        await exporter.ExportStudent(groupVM: groupVM);

        // Assert
        Assert.True(File.Exists(filePath));
    }

    [Fact]
    public async Task ExportStudent_GroupViewModelViewStudents_CreatesPopulatedCsvFile()
    {
        // Arrange
        Group group = ModelGenerator.GetPopulatedGroup(
            courseName: "CsvExporterCourseTest",
            groupName: "CsvExporterGroupTest");
        GroupViewModel groupVM = new(group);
        CsvExporter exporter = new(clock: fixture.Mock.Object, folderPath: fixture.DirPath);

        const string fileName = "Students of CsvExporterGroupTest(2023.08.14 06-30-15.7500).csv";
        string filePath = fixture.DirPath + "/" + fileName;

        const string expected =
            """
            LocalId, FirstName, LastName
            1,John,Doe
            2,Alex,Jordan
            
            """;

        // Act
        await exporter.ExportStudent(groupVM: groupVM);

        // Assert
        Assert.Equal(expected, await File.ReadAllTextAsync(filePath));
    }
}