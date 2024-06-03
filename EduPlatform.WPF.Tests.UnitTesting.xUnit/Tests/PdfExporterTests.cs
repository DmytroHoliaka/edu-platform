using EduPlatform.Domain.Models;
using EduPlatform.WPF.Service.DataExport;
using EduPlatform.WPF.Tests.UnitTesting.xUnit.Fixtures;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using Xunit;

namespace EduPlatform.WPF.Tests.UnitTesting.xUnit.Tests;

public class PdfExporterTests(PdfExporterFixture fixture) : IClassFixture<PdfExporterFixture>
{
    [Fact]
    public async Task ExportStudent_NullInput_ThrowsArgumentNullException()
    {
        // Arrange
        GroupViewModel? groupVM = null;
        PdfExporter exporter = new(clock: fixture.Mock.Object, folderPath: fixture.DirPath);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await exporter.ExportStudent(groupVM: groupVM));
    }

    [Fact]
    public async Task ExportStudent_GroupViewModelPlug_CreatesEmptyPdfFile()
    {
        // Arrange
        Group group = new()
        {
            Name = "PdfExporterGroupTest"
        };

        GroupViewModel groupVM = new(group);
        PdfExporter exporter = new(clock: fixture.Mock.Object, folderPath: fixture.DirPath);

        const string fileName = "Students of PdfExporterGroupTest(2024.09.24 11-45-12.2500).pdf";
        string filePath = fixture.DirPath + "/" + fileName;

        // Act
        await exporter.ExportStudent(groupVM: groupVM);

        // Assert
        Assert.True(File.Exists(filePath));
    }
}