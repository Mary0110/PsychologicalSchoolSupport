using PsychologicalSupportPlatform.Report.Application.DTOs;
using PsychologicalSupportPlatform.Report.Application.Interfaces;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace PsychologicalSupportPlatform.Report.Application.Services;

public class GenerateMonthlyReportService : IGenerateMonthlyReportService
{
    public MemoryStream GenerateMonthlyReport(List<ReportDTO> reportMeetupDtos)
    {
        var memoryStream = new MemoryStream();
        using (DocX document = DocX.Create(memoryStream))
        {
            var meetupsMonth = reportMeetupDtos[0].DateTime.Value.Month;
            document.InsertParagraph($"{meetupsMonth} meetings report")
                .FontSize(20)
                .Alignment = Alignment.center;
            document.InsertParagraph($"{reportMeetupDtos.Count} meetups took place")
                .FontSize(12);
            document.InsertParagraph($"{DateTime.Today.Date:dd:MM:yyyy}")
                .FontSize(12);
            
            document.SaveAs(memoryStream);
        }
        memoryStream.Seek(0, SeekOrigin.Begin);

        return memoryStream;
    }
}
