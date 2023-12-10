using MapsterMapper;
using PsychologicalSupportPlatform.Common.Errors;
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
    private readonly IMinioRepository _minioRepository;

    public MonthlyReportService(IMapper mapper, IMonthlyReportRepository monthlyReportRepository, IReportRepository reportRepository,
        IGenerateMonthlyReportService generateReportService, IMinioRepository minioRepository)
    {
        _mapper = mapper;
        _reportRepository = reportRepository;
        _monthlyReportRepository = monthlyReportRepository;
        _generateReportService = generateReportService;
        _minioRepository = minioRepository;
    }

    public async Task<int> AddMonthlyReportAsync()
    {
        var meetupReports = await _reportRepository.GetLastMonthReportsAsync();
        var meetupReportDtos = _mapper.Map<List<ReportDTO>>(meetupReports);
        var stream = _generateReportService.GenerateMonthlyReport(meetupReportDtos);
        
        var monthlyReport = new MonthlyReport{Date = DateTime.Now};
        var added = await _monthlyReportRepository.AddAsync(monthlyReport);
        await _reportRepository.SaveAsync();
        
        added.Name = $"report{added.Id.ToString()}.docx";
        await _monthlyReportRepository.UpdateAsync(added);
        await _reportRepository.SaveAsync();

        await _minioRepository.UploadReportAsync(stream, added.Name);

        return added.Id;
    }

    public async Task<MemoryStream> GetMonthlyReportAsync(GetMonthlyReportDTO dto, CancellationToken token)
    {
        var report = await _monthlyReportRepository.GetAsync(r => r.Date.Month == dto.Month);

        if (report is null)
        {
            throw new EntityNotFoundException(nameof(dto.Month));
        }
        
        var stream = await _minioRepository.DownloadReportAsync(report.Name!, token);

        return stream;
    }
}
