using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using EduPlatform.Domain.Models;
using EduPlatform.WPF.Service.DataExport;
using EduPlatform.WPF.Tests.UnitTesting.xUnit.Fixtures;
using EduPlatform.WPF.Tests.UnitTesting.xUnit.Utilities;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using Xunit;

namespace EduPlatform.WPF.Tests.UnitTesting.xUnit.Tests;

public class DocxExporterTests(DocxExporterFixtures fixture) : IClassFixture<DocxExporterFixtures>
{
    [Fact]
    public async Task ExportStudent_NullInput_ThrowsArgumentNullException()
    {
        // Arrange
        GroupViewModel? groupVM = null;
        DocxExporter exporter = new(clock: fixture.Mock.Object, folderPath: fixture.DirPath);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await exporter.ExportStudent(groupVM: groupVM));
    }

    [Fact]
    public async Task ExportStudent_GroupViewModelPlug_CreatesEmptyDocxFile()
    {
        // Arrange
        Group group = ModelGenerator.GetPlugGroup(groupName: "DocxExporterGroupTest");

        GroupViewModel groupVM = new(group);
        DocxExporter exporter = new(clock: fixture.Mock.Object, folderPath: fixture.DirPath);

        const string fileName = "Students of DocxExporterGroupTest(2024.09.24 11-45-12.2500).docx";
        string filePath = fixture.DirPath + "/" + fileName;

        // Act
        await exporter.ExportStudent(groupVM: groupVM);

        // Assert
        Assert.True(File.Exists(filePath));
    }

    [Fact]
    public async Task ExportStudent_GroupViewModelViewStudents_CreatesPopulatedDocxFile()
    {
        // Arrange
        Group group = ModelGenerator.GetPopulatedGroup(
            courseName: "DocxExporterCourseTest", 
            groupName: "DocxExporterGroupTest");
        GroupViewModel groupVM = new(group);
        DocxExporter exporter = new(clock: fixture.Mock.Object, folderPath: fixture.DirPath);

        const string fileName = "Students of DocxExporterGroupTest(2024.09.24 11-45-12.2500).docx";
        string filePath = fixture.DirPath + "/" + fileName;

        const string expected =
            "DocxExporterCourseTest" +
            "DocxExporterGroupTest" +
            "John Doe" +
            "Alex Jordan";

        // Act
        await exporter.ExportStudent(groupVM: groupVM);

        // Assert
        Assert.Equal(expected, GetTextFromDocx(filePath));
    }

    private static string? GetTextFromDocx(string path)
    {
        using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(path: path, isEditable: false))
        {
            Body? body = wordDoc.MainDocumentPart?.Document.Body;
            return body?.InnerText;
        }
    }
}