namespace EduPlatform.WPF.Tests.UnitTesting.xUnit.Fixtures
{
    public class CsvValidatorFixture : IDisposable
    {
        public string DirPath { get; }

        public CsvValidatorFixture()
        {
            DirPath = "./CsvValidatorFixtureTests";

            if (Directory.Exists(path: DirPath))
            {
                Directory.Delete(path: DirPath, recursive: true);
            }

            Directory.CreateDirectory(path: DirPath); // Do nothing if already exists
        }

        public void Dispose()
        {
            Directory.Delete(path: DirPath, recursive: true); // Throw exception if it doesn't exist
        }
    }
}