using MapsterMapper;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Common.Interfaces;
using PsychologicalSupportPlatform.Report.Application.DTOs;
using PsychologicalSupportPlatform.Report.Application.Interfaces;
using PsychologicalSupportPlatform.Report.Domain.Entities;

namespace PsychologicalSupportPlatform.Report.Application.Services;

public class ReportService: IReportService
{
    private readonly IMapper _mapper;
    private readonly IReportRepository _repository;
    private readonly IGenerateReportService _generateReportService;
    private readonly IUserGrpcClient _userGrpcClient;

    public ReportService(IMapper mapper, IReportRepository repository,
        IGenerateReportService generateReportService, IUserGrpcClient userGrpcClient)
    {
        _mapper = mapper;
        _repository = repository;
        _generateReportService = generateReportService;
        _userGrpcClient = userGrpcClient;
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

    public async Task<MemoryStream> GenerateReportAsync(GenerateReportDTO dto, CancellationToken token)
    {
        var report = await _repository.GetAsync(r => r.MeetupId == dto.meetId);
        
        if (report is null)
        {
            throw new EntityNotFoundException(paramname: nameof(dto.meetId));
        }
        
        report.Comments = dto.comment;
        report.CreatorId = dto.creatorId;
        report.Conclusion = dto.conclusion;
        await _repository.UpdateAsync(report);
        await _repository.SaveAsync();
        var creatorReply = await _userGrpcClient.CheckUserNameAsync(dto.creatorId, token);
        var studentReply = await _userGrpcClient.CheckUserNameAsync(report.StudentId, token);
        var reportMeetupDto = new ReportMeetupDTO(
            creatorReply.Name, 
            creatorReply.Surname, 
            creatorReply.Patronymic, 
            DateOnly.FromDateTime(DateTime.Now), 
            studentReply.Name, 
            studentReply.Surname, 
            studentReply.Patronymic,  
            dto.comment, 
            dto.conclusion
            );
        var stream = _generateReportService.GenerateReport(reportMeetupDto);
        
        return stream;
    }
}
