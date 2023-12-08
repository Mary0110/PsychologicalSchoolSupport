using MapsterMapper;
using Microsoft.AspNetCore.Hosting;
using PsychologicalSupportPlatform.Report.Application.DTOs;
using PsychologicalSupportPlatform.Report.Application.Interfaces;
using PsychologicalSupportPlatform.Report.Domain.Entities;

namespace PsychologicalSupportPlatform.Report.Application.Services;

public class MonthlyReportService: IMonthlyReportService
{
    private readonly IMapper _mapper;
    private readonly IMonthlyReportRepository _monthlyReportRepository;
    private readonly IReportRepository _reportRepository;
    private readonly IGenerateMonthlyReportService _generateReportService;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public MonthlyReportService(IMapper mapper, IMonthlyReportRepository monthlyReportRepository, IReportRepository reportRepository,
        IGenerateMonthlyReportService generateReportService,IWebHostEnvironment hostingEnvironment)
    {
        _mapper = mapper;
        _reportRepository = reportRepository;
        _monthlyReportRepository = monthlyReportRepository;
        _generateReportService = generateReportService;
        _hostingEnvironment = hostingEnvironment;
    }

    public async Task<int> AddMonthlyReportAsync()
    {
        var meetupReports = await _reportRepository.GetLastMonthReportsAsync();
        var meetupReportDtos = _mapper.Map<List<ReportDTO>>(meetupReports);

        var stream = _generateReportService.GenerateMonthlyReport(meetupReportDtos);

        var monthlyReport = new MonthlyReport();
        var added = await _monthlyReportRepository.AddAsync(monthlyReport);
        await _reportRepository.SaveAsync();
        added.Filepath = Path.Combine(_hostingEnvironment.ContentRootPath, $"{monthlyReport.Id.ToString()}.docx");
        await _monthlyReportRepository.UpdateAsync(added);
        await _reportRepository.SaveAsync();
        
        using (var fileStreamOutput = new FileStream(monthlyReport.Filepath, FileMode.Create))
        {
            await stream.CopyToAsync(fileStreamOutput);
        }
        
        return added.Id;
    }
}
