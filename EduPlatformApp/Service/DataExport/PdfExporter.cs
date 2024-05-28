using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModels;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace EduPlatform.WPF.Service.DataExport;

public class PdfExporter : DataExporter
{
    private const string CourseFont = "Algerian";
    private const float CourseFontSize = 26;

    private const string GroupFont = "Blackadder ITC";
    private const float GroupFontSize = 28;

    private const string StudentFont = "Bell MT";
    private const float StudentFontSize = 16;

    private const string FooterFont = "Blackadder ITC";
    private const float FooterFontSize = 20;

    private const string ForegroundColor = "#3e214a";
    private const string BackgroundColor = "#FFF3E0";

    private const float PageMargin = 2;
    private const float ContentVerticalPadding = 1;
    private const float ItemRowSpacing = 0.25f;
    private const float ItemVerticalSpacing = 0.35f;

    public override async Task ExportStudent(GroupViewModel groupVM)
    {
        string filePath = GetFilePath(
            groupName: groupVM.GroupName,
            extension: ".pdf");

        await Task.Run(
            () =>
            {
                Document.Create(
                        container =>
                        {
                            container.Page(
                                page =>
                                {
                                    ConfigurePageStyle(page);

                                    page.Header().Element(
                                        header =>
                                        {
                                            header.Column(
                                                column => { ConfigureTitles(groupVM, column); });
                                        });

                                    page.Content()
                                        .PaddingVertical(ContentVerticalPadding, Unit.Centimetre)
                                        .Column(
                                            column =>
                                            {
                                                column.Spacing(ItemVerticalSpacing, Unit.Centimetre);
                                                ConfigureNumberedList(groupVM.StudentVMs.ToList(), column);
                                            });

                                    page.Footer()
                                        .AlignCenter()
                                        .Text(ConfigureFooter);

                                });
                        })
                    .GeneratePdf(filePath);
            });
    }

    private static void ConfigureFooter(TextDescriptor x)
    {
        TextStyle footerStyle = TextStyle.Default
            .FontSize(FooterFontSize)
            .FontFamily(FooterFont);

        x.Span("Page ")
            .FontSize(FooterFontSize)
            .Style(footerStyle);

        x.CurrentPageNumber()
            .Style(footerStyle);
    }

    private static void ConfigurePageStyle(PageDescriptor page)
    {
        page.Size(PageSizes.A4);
        page.Margin(PageMargin, Unit.Centimetre);
        page.PageColor(Color.FromHex(BackgroundColor));
        page.DefaultTextStyle(TextStyle.Default.FontColor(Color.FromHex(ForegroundColor)));
    }

    private static void ConfigureTitles(GroupViewModel groupVM, ColumnDescriptor column)
    {
        TextStyle courseTitleStyle = TextStyle.Default
            .FontSize(CourseFontSize)
            .FontFamily(CourseFont);

        TextStyle groupTitleStyle = TextStyle.Default
            .FontSize(GroupFontSize)
            .FontFamily(GroupFont);


        column.Item()
            .Text(groupVM.CourseVM?.CourseName ?? "<Course not specified>")
            .Style(courseTitleStyle)
            .AlignCenter();


        column.Item()
            .Text(groupVM.GroupName)
            .Style(groupTitleStyle)
            .AlignCenter();
    }

    private static void ConfigureNumberedList(List<StudentViewModel> studentVMs, ColumnDescriptor column)
    {
        TextStyle itemStyle = TextStyle.Default
            .FontSize(StudentFontSize)
            .FontFamily(StudentFont);

        foreach (int i in Enumerable.Range(1, studentVMs.Count))
        {
            column.Item().Row(
                row =>
                {
                    row.Spacing(ItemRowSpacing, Unit.Centimetre);
                    row.AutoItem().Text($"{i}.").Style(itemStyle);

                    string fullName = $"{studentVMs[i - 1].FirstName} {studentVMs[i - 1].LastName}";
                    row.RelativeItem().Text(fullName).Style(itemStyle);
                });
        }
    }
}