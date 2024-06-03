using EduPlatform.WPF.Service.Validators;
using EduPlatform.WPF.Tests.UnitTesting.xUnit.Comparers;
using EduPlatform.WPF.Tests.UnitTesting.xUnit.Fixtures;
using Xunit;

namespace EduPlatform.WPF.Tests.UnitTesting.xUnit.Tests;

public class CsvValidatorTests(CsvValidatorFixture fixture) : IClassFixture<CsvValidatorFixture>
{
    [Fact]
    public void GetFileErrors_NullInput_ReturnsErrorList()
    {
        // Act
        const string? path = null;
        CsvValidator validator = new();
        IEnumerable<RowError> expected =
        [
            new RowError(
                lineNumber: -1,
                columnName: "<not specified>",
                explanation: "Incorrect file path.")
        ];


        // Arrange
        IEnumerable<RowError> actual = validator.GetFileErrors(path: path);

        // Assert
        Assert.Equal(expected, actual, new RowErrorEqualityComparer());
    }

    [Fact]
    public void GetFileErrors_IncorrectPath_ReturnsErrorList()
    {
        // Act
        const string? path = "<unknown path>";
        CsvValidator validator = new();
        IEnumerable<RowError> expected =
        [
            new RowError(
                lineNumber: -1,
                columnName: "<not specified>",
                explanation: "There is no file at the specified path")
        ];


        // Arrange
        IEnumerable<RowError> actual = validator.GetFileErrors(path: path);

        // Assert
        Assert.Equal(expected, actual, new RowErrorEqualityComparer());
    }

    [Theory]
    [InlineData("IncorrectExtensionFile.")]
    [InlineData("IncorrectExtensionFile.a")]
    [InlineData("IncorrectExtensionFile.abc")]
    public void GetFileErrors_IncorrectFileExtension_ReturnsErrorList(string fileName)
    {
        // Act
        string path = $"{fixture.DirPath}/{fileName}";
        FileStream fs = File.Create(path);
        fs.Dispose();

        CsvValidator validator = new();
        IEnumerable<RowError> expected =
        [
            new RowError(
                lineNumber: -1,
                columnName: "<not specified>",
                explanation: "Incorrect file extension")
        ];


        // Arrange
        IEnumerable<RowError> actual = validator.GetFileErrors(path: path);

        // Assert
        Assert.Equal(expected, actual, new RowErrorEqualityComparer());
    }

    [Theory]
    [MemberData(nameof(TestData.GetDataForIncorrectHeaders), MemberType = typeof(TestData))]
    public void GetFileErrors_IncorrectHeader_ReturnsErrorList(string filePath, string content,
        IEnumerable<RowError> expected)
    {
        // Act
        File.WriteAllText(filePath, content);
        CsvValidator validator = new();

        // Arrange
        IEnumerable<RowError> actual = validator.GetFileErrors(path: filePath);

        // Assert
        Assert.Equal(expected, actual, new RowErrorEqualityComparer());
    }

    [Theory]
    [MemberData(nameof(TestData.GetDataForIncorrectBody), MemberType = typeof(TestData))]
    public void GetFileErrors_IncorrectBody_ReturnsErrorList(string filePath, string content,
        IEnumerable<RowError> expected)
    {
        // Act
        File.WriteAllText(filePath, content);
        CsvValidator validator = new();

        // Arrange
        IEnumerable<RowError> actual = validator.GetFileErrors(path: filePath);

        // Assert
        Assert.Equal(expected, actual, new RowErrorEqualityComparer());
    }

    private static class TestData
    {
        private const int CorrectColumnCount = 3;
        private const string FirstColumn = "LocalId";
        private const string SecondColumn = "FirstName";
        private const string ThirdColumn = "LastName";
        private const string CorrectHeader = $"{FirstColumn},{SecondColumn},{ThirdColumn}";

        public static IEnumerable<object?[]> GetDataForIncorrectHeaders()
        {
            const string emptyHeaderFileName = "EmptyHeader.csv";
            string emptyHeader = string.Empty;
            IEnumerable<RowError> emptyHeaderErrors =
            [
                new RowError(
                    lineNumber: 1,
                    columnName: "<not specified>",
                    explanation: "The file is empty")
            ];

            const string incorrectColumnCountFileName1 = "IncorrectColumnCount1.csv";
            const string incorrectColumnCountHeader1 = "First,Second";
            IEnumerable<RowError> incorrectColumnCountErrors1 =
            [
                new RowError(
                    lineNumber: 1,
                    columnName: "<not specified>",
                    explanation: $"Incorrect amount of columns (must be {CorrectColumnCount})")
            ];

            const string incorrectColumnCountFileName2 = "IncorrectColumnCount2.csv";
            const string incorrectColumnCountHeader2 = "First,Second,Third,Fourth";
            IEnumerable<RowError> incorrectColumnCountErrors2 =
            [
                new RowError(
                    lineNumber: 1,
                    columnName: "<not specified>",
                    explanation: $"Incorrect amount of columns (must be {CorrectColumnCount})")
            ];

            const string incorrectFirstColumnFileName = "incorrectFirstColumn.csv";
            const string incorrectFirstColumnHeader = $"Unknown,{SecondColumn},{ThirdColumn}";
            IEnumerable<RowError> incorrectFirstColumnErrors =
            [
                new RowError(
                    lineNumber: 1,
                    columnName: FirstColumn,
                    explanation: $"Incorrect name of first column (must be {FirstColumn})")
            ];

            const string incorrectSecondColumnFileName = "incorrectSecondColumn.csv";
            const string incorrectSecondColumnHeader = $"{FirstColumn},Unknown,{ThirdColumn}";
            IEnumerable<RowError> incorrectSecondColumnErrors =
            [
                new RowError(
                    lineNumber: 1,
                    columnName: SecondColumn,
                    explanation: $"Incorrect name of second column (must be {SecondColumn})")
            ];

            const string incorrectThirdColumnFileName = "incorrectFirstColumn.csv";
            const string incorrectThirdColumnHeader = $"{FirstColumn},{SecondColumn},Unknown";
            IEnumerable<RowError> incorrectThirdColumnErrors =
            [
                new RowError(
                    lineNumber: 1,
                    columnName: ThirdColumn,
                    explanation: $"Incorrect name of third column (must be {ThirdColumn})")
            ];

            const string incorrectFirstAndThirdColumnFileName = "incorrectFirstAndThirdColumn.csv";
            const string incorrectFirstAndThirdColumnHeader = $"Unknown,{SecondColumn},Unknown";
            IEnumerable<RowError> incorrectFirstAndThirdColumnErrors =
            [
                new RowError(
                    lineNumber: 1,
                    columnName: FirstColumn,
                    explanation: $"Incorrect name of first column (must be {FirstColumn})"),
                new RowError(
                    lineNumber: 1,
                    columnName: ThirdColumn,
                    explanation: $"Incorrect name of third column (must be {ThirdColumn})")
            ];

            const string incorrectAllColumnsFileName = "incorrectAllColumns.csv";
            const string incorrectAllColumnsHeader = $"Unknown,Unknown,Unknown";
            IEnumerable<RowError> incorrectAllColumnsErrors =
            [
                new RowError(
                    lineNumber: 1,
                    columnName: FirstColumn,
                    explanation: $"Incorrect name of first column (must be {FirstColumn})"),
                new RowError(
                    lineNumber: 1,
                    columnName: SecondColumn,
                    explanation: $"Incorrect name of second column (must be {SecondColumn})"),
                new RowError(
                    lineNumber: 1,
                    columnName: ThirdColumn,
                    explanation: $"Incorrect name of third column (must be {ThirdColumn})")
            ];

            return
            [
                [
                    emptyHeaderFileName,
                    emptyHeader,
                    emptyHeaderErrors
                ],
                [
                    incorrectColumnCountFileName1,
                    incorrectColumnCountHeader1,
                    incorrectColumnCountErrors1
                ],
                [
                    incorrectColumnCountFileName2,
                    incorrectColumnCountHeader2,
                    incorrectColumnCountErrors2
                ],
                [
                    incorrectFirstColumnFileName,
                    incorrectFirstColumnHeader,
                    incorrectFirstColumnErrors,
                ],
                [
                    incorrectSecondColumnFileName,
                    incorrectSecondColumnHeader,
                    incorrectSecondColumnErrors,
                ],
                [
                    incorrectThirdColumnFileName,
                    incorrectThirdColumnHeader,
                    incorrectThirdColumnErrors,
                ],
                [
                    incorrectFirstAndThirdColumnFileName,
                    incorrectFirstAndThirdColumnHeader,
                    incorrectFirstAndThirdColumnErrors,
                ],
                [
                    incorrectAllColumnsFileName,
                    incorrectAllColumnsHeader,
                    incorrectAllColumnsErrors,
                ]
            ];
        }

        public static IEnumerable<object?[]> GetDataForIncorrectBody()
        {
            const string incorrectColumnCountFileName1 = "IncorrectColumnCount1.csv";
            const string incorrectColumnCountContent1 =
                $"""
                 {CorrectHeader}
                 1,John
                 """;
            IEnumerable<RowError> incorrectColumnCountErrors1 =
            [
                new RowError(
                    lineNumber: 2,
                    columnName: $"<not specified>",
                    explanation: "Incorrect number of columns")
            ];

            const string incorrectColumnCountFileName2 = "IncorrectColumnCount2.csv";
            const string incorrectColumnCountContent2 =
                $"""
                 {CorrectHeader}
                 1,John,Doe,UnnecessaryColumn
                 """;
            IEnumerable<RowError> incorrectColumnCountErrors2 =
            [
                new RowError(
                    lineNumber: 2,
                    columnName: $"<not specified>",
                    explanation: "Incorrect number of columns")
            ];

            const string incorrectIdFileName1 = "IncorrectId1.csv";
            const string incorrectIdContent1 =
                $"""
                 {CorrectHeader}
                 Text,John,Doe
                 """;
            IEnumerable<RowError> incorrectIdErrors1 =
            [
                new RowError(
                    lineNumber: 2,
                    columnName: FirstColumn,
                    explanation: "Incorrect identifier value")
            ];

            const string incorrectIdFileName2 = "IncorrectId2.csv";
            const string incorrectIdContent2 =
                $"""
                 {CorrectHeader}
                 ,John,Doe
                 """;
            IEnumerable<RowError> incorrectIdErrors2 =
            [
                new RowError(
                    lineNumber: 2,
                    columnName: FirstColumn,
                    explanation: "Incorrect identifier value")
            ];

            const string incorrectIdFileName3 = "IncorrectId3.csv";
            const string incorrectIdContent3 =
                $"""
                 {CorrectHeader}
                 -1,John,Doe
                 """;
            IEnumerable<RowError> incorrectIdErrors3 =
            [
                new RowError(
                    lineNumber: 2,
                    columnName: FirstColumn,
                    explanation: "Incorrect identifier value")
            ];

            const string incorrectIdFileName4 = "IncorrectId4.csv";
            const string incorrectIdContent4 =
                $"""
                 {CorrectHeader}
                 1,John,Doe
                 1,Alex,Jordan
                 """;
            IEnumerable<RowError> incorrectIdErrors4 =
            [
                new RowError(
                    lineNumber: 3,
                    columnName: FirstColumn,
                    explanation: "The value of the identifier is not unique")
            ];

            const string incorrectSecondColumnFileName1 = "IncorrectSecondColumn1.csv";
            const string incorrectSecondColumnContent1 =
                $"""
                 {CorrectHeader}
                 1,,Doe
                 """;
            IEnumerable<RowError> incorrectSecondColumnErrors1 =
            [
                new RowError(
                    lineNumber: 2,
                    columnName: SecondColumn,
                    explanation: "First name cannot be empty or white space")
            ];

            const string incorrectSecondColumnFileName2 = "IncorrectSecondColumn2.csv";
            const string incorrectSecondColumnContent2 =
                $"""
                 {CorrectHeader}
                 1,{"\t\t"},Doe
                 """;
            IEnumerable<RowError> incorrectSecondColumnErrors2 =
            [
                new RowError(
                    lineNumber: 2,
                    columnName: SecondColumn,
                    explanation: "First name cannot be empty or white space")
            ];

            const string incorrectThirdColumnFileName1 = "IncorrectThirdColumn1.csv";
            const string incorrectThirdColumnContent1 =
                $"""
                 {CorrectHeader}
                 1,John,
                 """;
            IEnumerable<RowError> incorrectThirdColumnErrors1 =
            [
                new RowError(
                    lineNumber: 2,
                    columnName: ThirdColumn,
                    explanation: "Second name cannot be empty or white space")
            ];

            const string incorrectThirdColumnFileName2 = "IncorrectThirdColumn2.csv";
            const string incorrectThirdColumnContent2 =
                $"""
                 {CorrectHeader}
                 1,John,{"\t\t"}
                 """;
            IEnumerable<RowError> incorrectThirdColumnErrors2 =
            [
                new RowError(
                    lineNumber: 2,
                    columnName: ThirdColumn,
                    explanation: "Second name cannot be empty or white space")
            ];

            return
            [
                [
                    incorrectColumnCountFileName1,
                    incorrectColumnCountContent1,
                    incorrectColumnCountErrors1
                ],
                [
                    incorrectColumnCountFileName2,
                    incorrectColumnCountContent2,
                    incorrectColumnCountErrors2
                ],
                [
                    incorrectIdFileName1,
                    incorrectIdContent1,
                    incorrectIdErrors1
                ],
                [
                    incorrectIdFileName2,
                    incorrectIdContent2,
                    incorrectIdErrors2
                ],
                [
                    incorrectIdFileName3,
                    incorrectIdContent3,
                    incorrectIdErrors3
                ],
                [
                    incorrectIdFileName4,
                    incorrectIdContent4,
                    incorrectIdErrors4
                ],
                [
                    incorrectSecondColumnFileName1,
                    incorrectSecondColumnContent1,
                    incorrectSecondColumnErrors1
                ],
                [
                    incorrectSecondColumnFileName2,
                    incorrectSecondColumnContent2,
                    incorrectSecondColumnErrors2
                ],
                [
                    incorrectThirdColumnFileName1,
                    incorrectThirdColumnContent1,
                    incorrectThirdColumnErrors1
                ],
                [
                    incorrectThirdColumnFileName2,
                    incorrectThirdColumnContent2,
                    incorrectThirdColumnErrors2
                ]
            ];
        }
    }
}