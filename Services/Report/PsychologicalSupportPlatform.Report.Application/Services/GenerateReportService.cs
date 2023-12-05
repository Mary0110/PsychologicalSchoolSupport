using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Report.Application.Interfaces;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace PsychologicalSupportPlatform.Report.Application.Services;

public class GenerateReportService:IGenerateReportService
{
    public MemoryStream GenerateReport(ReportMeetupDTO reportMeetupDtos)
    {
        var memoryStream = new MemoryStream();
        using (DocX document = DocX.Create(memoryStream))
        {
            document.InsertParagraph($"Student: {reportMeetupDtos.StudentSurname} " +
                                     $"{reportMeetupDtos.StudentName} " +
                                     $"{reportMeetupDtos.StudentPatronymic}")
                .FontSize(16);
            document.InsertParagraph($"Date: {reportMeetupDtos.Date:dd.MM.yyyy}")
                .FontSize(14);
            document.InsertParagraph();

            document.InsertParagraph($"Comments: {reportMeetupDtos.Comments}");
            document.InsertParagraph($"Conclusion: {reportMeetupDtos.Conclusion}");
            
            document.InsertParagraph($"Report Created by: {reportMeetupDtos.creatorName} " +
                                     $"{reportMeetupDtos.creatorSurname} {reportMeetupDtos.creatorPatronymic}")
                .Bold()
                .Alignment = Alignment.right;
            document.InsertParagraph($"{DateTime.Today:dd.MM.yyyy}")
                .Alignment = Alignment.right;

            document.SaveAs(memoryStream);
        }
        memoryStream.Seek(0, SeekOrigin.Begin);
        
        return memoryStream;
    }
}
