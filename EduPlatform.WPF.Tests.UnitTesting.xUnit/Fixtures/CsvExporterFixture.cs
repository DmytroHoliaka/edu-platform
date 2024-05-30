using EduPlatform.WPF.Service.Time;
using Moq;

namespace EduPlatform.WPF.Tests.UnitTesting.xUnit.Fixtures;

public class CsvExporterFixture : IDisposable
{
    public string DirPath { get; }
    public Mock<IClock> Mock { get; }

    public CsvExporterFixture()
    {
        Mock = new Mock<IClock>();
        Mock.Setup(clock => clock.Now).Returns(new DateTime(2023, 8, 14, 18, 30, 15, 750));
        
        DirPath = "./CsvExporterFixtureTests";

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