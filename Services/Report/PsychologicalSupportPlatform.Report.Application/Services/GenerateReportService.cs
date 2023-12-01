using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Report.Application.DTOs;
using PsychologicalSupportPlatform.Report.Application.Interfaces;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace PsychologicalSupportPlatform.Report.Application.Services;

public class GenerateReportService:IGenerateReportService
{
    public void GenerateReport(string filePath, string creatorName, ReportMeetupDTO reportMeetupDtos)
    {
        using (DocX document = DocX.Create(filePath))
        {
            document.InsertParagraph();
            
            document.InsertParagraph($"Student: {reportMeetupDtos.StudentName}, {reportMeetupDtos.StudentSurname}")
                .FontSize(16);
            document.InsertParagraph($"Date: {reportMeetupDtos.Date:dd.MM.yyyy}")
                .FontSize(14);
            document.InsertParagraph();

            document.InsertParagraph($"Comments: {reportMeetupDtos.Comments}");
            document.InsertParagraph($"Conclusion: {reportMeetupDtos.Conclusion}");
            
            document.InsertParagraph($"Report Created by: {creatorName}")
                .Bold()
                .Alignment = Alignment.right;

            document.Save();
        }
    }
}
