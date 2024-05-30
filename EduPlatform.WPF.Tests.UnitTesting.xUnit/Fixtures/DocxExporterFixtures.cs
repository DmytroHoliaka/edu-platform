using EduPlatform.WPF.Service.Time;
using Moq;

namespace EduPlatform.WPF.Tests.UnitTesting.xUnit.Fixtures;

public class DocxExporterFixtures : IDisposable
{
    public string DirPath { get; }
    public Mock<IClock> Mock { get; }

    public DocxExporterFixtures()
    {
        Mock = new Mock<IClock>();
        Mock.Setup(clock => clock.Now).Returns(new DateTime(2024, 9, 24, 11, 45, 12, 250));

        DirPath = "./DocxExporterFixturesTest";

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