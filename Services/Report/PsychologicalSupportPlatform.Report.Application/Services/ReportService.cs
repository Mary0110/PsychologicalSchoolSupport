using MapsterMapper;
using Microsoft.AspNetCore.Hosting;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Report.Application.DTOs;
using PsychologicalSupportPlatform.Report.Application.Interfaces;
using PsychologicalSupportPlatform.Report.Domain.Entities;

namespace PsychologicalSupportPlatform.Report.Application.Services;

public class ReportService: IReportService
{
    private readonly IMapper _mapper;
    private readonly IReportRepository _repository;
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly IGenerateReportService _service;
    
    public ReportService(IMapper mapper, IReportRepository repository, IWebHostEnvironment hostingEnvironment,
        IGenerateReportService service)
    {
        _mapper = mapper;
        _repository = repository;
        _hostingEnvironment = hostingEnvironment;
        _service = service;
    }
    
    public async Task<List<MeetupReportDTO>> GetAllMeetupReportsAsync(int pageNumber, int pageSize)
    {
        var meetupReport = await _repository.GetAllAsync(pageNumber, pageSize);

        if (meetupReport is null)
        {
            throw new EntityNotFoundException();
        }

        var meetupReportModels = _mapper.Map<List<MeetupReportDTO>>(meetupReport);

        return meetupReportModels;    
    }

    public async Task<MeetupReportDTO?> GetMeetupReportAsync(int id)
    {
        var meetupReport = await _repository.GetByIdAsync(id);

        if (meetupReport is null)
        {
            throw new EntityNotFoundException();
        }

        var meetupReportModel = _mapper.Map<MeetupReportDTO>(meetupReport);

        return meetupReportModel;
    }

    public async Task<List<MeetupReportDTO>> GetMeetupReportsByCreatorIdAsync(int creatorId, int pageNumber, int pageSize)
    {
        var meetupReports = await _repository.GetAllAsync(
            r => r.CreatorId == creatorId, pageNumber, pageSize
            );

        if (meetupReports is null)
        {
            throw new EntityNotFoundException();
        }

        var meetupReportModel = _mapper.Map<List<MeetupReportDTO>>(meetupReports);

        return meetupReportModel;    
    }

    public async Task<List<MeetupReportDTO>> GetMeetupReportsByDateAsync(DateTime date, int pageNumber, int pageSize)
    {
        var meetupReports = await _repository.GetAllAsync(
            r => r.DateTime >= date, pageNumber, pageSize
        );

        if (meetupReports is null)
        {
            throw new EntityNotFoundException();
        }

        var meetupReportModel = _mapper.Map<List<MeetupReportDTO>>(meetupReports);

        return meetupReportModel;        
    }

    public async Task<List<MeetupReportDTO>> GetMeetupReportsFromDateAsync(DateTime date, int pageNumber, int pageSize)
    {
        var meetupReports = await _repository.GetAllAsync(
            r => r.DateTime >= date, pageNumber, pageSize
            );

        if (meetupReports is null)
        {
            throw new EntityNotFoundException();
        }

        var meetupReportModel = _mapper.Map<List<MeetupReportDTO>>(meetupReports);

        return meetupReportModel;
    }

    public async Task<int> DeleteMeetupReportAsync(int id)
    {
        var meetupReport = await _repository.GetAsync(m => m.Id == id);

        if (meetupReport is null)
        {
            throw new EntityNotFoundException(paramname: nameof(id));
        }
        
        await _repository.DeleteAsync(meetupReport);
        await _repository.SaveAsync();

        return meetupReport.Id;
    }

    public Task EditMeetupReportAsync(MeetupReportDTO form)
    {
        throw new NotImplementedException();
    }

    public async Task<int> AddMeetupReportAsync(MeetupMessageObject form)
    {
        var newMeetupReport = _mapper.Map<MeetupReport>(form);
    
        if (newMeetupReport is null)
        {
            throw new WrongRequestDataException();
        }
        
        var meetupReport = await _repository.AddAsync(newMeetupReport);
        await _repository.SaveAsync();
    
        return meetupReport.Id;
    }

    public async Task GenerateReportAsync(int meetId, string comment, string id)
    {
        var report = await _repository.GetAsync(r => r.MeetupId == meetId);
        
        if (report is null)
        {
            throw new EntityNotFoundException(paramname: nameof(meetId));
        }

        report.Comments = comment;
        report.Filepath = Path.Combine(_hostingEnvironment.WebRootPath, 
            $"Services/Report/Files/{report.Id}");

        await _repository.UpdateAsync(report);
        await _repository.SaveAsync();
        //todo:grpc for getting name
        var dto = new ReportMeetupDTO(DateOnly.FromDateTime(DateTime.Now), "Name", "Surname", comment, "conclusion");
        _service.GenerateReport(report.Filepath, id, dto);
    }
}
