using DocumentFormat.OpenXml.Packaging;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using DocumentFormat.OpenXml.Wordprocessing;
using EduPlatform.WPF.ViewModels.StudentsViewModels;

namespace EduPlatform.WPF.Service.DataExport;

public class DocxExporter : DataExporter
{
    private const string CourseFont = "Algerian";
    private const string CourseHalfPoints = "46";

    private const string GroupFont = "Blackadder ITC";
    private const string GroupHalfPoints = "50";

    private const string StudentFont = "Bell MT";
    private const string StudentHalfPoints = "28";

    public override async Task ExportStudent(GroupViewModel groupVM)
    {
        string filePath = GetFilePath(
            groupVM.GroupName,
            extension: ".docx");

        await Task.Run(
            () =>
            {
                using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(
                           filePath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
                {
                    MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                    mainPart.Document = new Document();
                    Body body = mainPart.Document.AppendChild(new Body());

                    AddTitle(
                        body: body,
                        titleText: groupVM.CourseVM?.CourseName ?? "<Course not specified>",
                        font: CourseFont,
                        halfPoints: CourseHalfPoints);

                    AddTitle(
                        body: body,
                        titleText: groupVM.GroupName,
                        font: GroupFont,
                        halfPoints: GroupHalfPoints);

                    AddNumberedList(
                        mainPart: mainPart,
                        body: body,
                        studentVMs: groupVM.StudentVMs.ToList());

                    mainPart.Document.Save();
                }
            });
    }

    private static void AddTitle(Body body, string titleText, string font, string halfPoints)
    {
        ParagraphProperties paragraphProperties = new(
            new Justification() { Val = JustificationValues.Center });

        RunProperties runProperties = new(
            new RunFonts() { Ascii = font },
            new FontSize() { Val = halfPoints });


        Text text = new(titleText);

        Run run = new(runProperties, text);

        Paragraph titleParagraph = new(paragraphProperties, run);

        body.Append(titleParagraph);
    }

    private static void AddNumberedList(MainDocumentPart mainPart, Body body, List<StudentViewModel> studentVMs)
    {
        NumberingDefinitionsPart numberingPart = mainPart.AddNewPart<NumberingDefinitionsPart>();
        numberingPart.Numbering = new Numbering();

        AbstractNum abstractNum = new() { AbstractNumberId = 1 };
        Level level = new(
            new NumberingFormat() { Val = NumberFormatValues.Decimal },
            new LevelText() { Val = "%1." },
            new StartNumberingValue() { Val = 1 })
        {
            LevelIndex = 0
        };

        abstractNum.Append(level);

        numberingPart.Numbering.Append(abstractNum);

        NumberingInstance numberingInstance = new(new AbstractNumId() { Val = 1 }) { NumberID = 1 };
        numberingPart.Numbering.Append(numberingInstance);

        foreach (StudentViewModel studentVM in studentVMs)
        {
            AddNumberedListItem(
                body: body,
                itemText: $"{studentVM.FirstName} {studentVM.LastName}",
                numberingId: 1,
                font: StudentFont,
                halfPoints: StudentHalfPoints);
        }
    }

    private static void AddNumberedListItem(Body body, string itemText, int numberingId, string font, string halfPoints)
    {
        ParagraphProperties paragraphProperties = new(
            new NumberingProperties(
                new NumberingLevelReference() { Val = 0 },
                new NumberingId() { Val = numberingId }));

        RunProperties runProperties = new(
            new RunFonts() { Ascii = font },
            new FontSize() { Val = halfPoints });

        Text text = new(itemText);
        Run run = new(runProperties.CloneNode(true), text);
        Paragraph paragraph = new(runProperties.CloneNode(true), paragraphProperties, run);

        body.Append(paragraph);
    }
}