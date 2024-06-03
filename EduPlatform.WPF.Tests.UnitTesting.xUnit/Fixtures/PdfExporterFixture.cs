using EduPlatform.WPF.Service.Time;
using Moq;
using QuestPDF.Infrastructure;

namespace EduPlatform.WPF.Tests.UnitTesting.xUnit.Fixtures;

public class PdfExporterFixture : IDisposable
{
    public string DirPath { get; }
    public Mock<IClock> Mock { get; }

    public PdfExporterFixture()
    {
        Mock = new Mock<IClock>();
        Mock.Setup(clock => clock.Now).Returns(new DateTime(2024, 9, 24, 11, 45, 12, 250));

        QuestPDF.Settings.License = LicenseType.Community;
        DirPath = "./PdfExporterFixtureTests";

        if (Directory.Exists(path: DirPath))
        {
            Directory.Delete(path: DirPath, recursive: true);
        }

        Directory.CreateDirectory(path: DirPath);
    }

    public void Dispose()
    {
        Directory.Delete(path: DirPath, recursive: true);
    }
}