using MapsterMapper;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Common.Interfaces;
using PsychologicalSupportPlatform.Edu.Application.DTOs.Tests;
using PsychologicalSupportPlatform.Edu.Application.Interfaces;
using PsychologicalSupportPlatform.Edu.Application.Interfaces.Tests;
using PsychologicalSupportPlatform.Edu.Domain.Entities.Tests;

namespace PsychologicalSupportPlatform.Edu.Application.Services;

public class TestService: ITestService
{
    private readonly ITestResultRepository _testResultRepository;
    private readonly ITestRepository _psychologicalTestRepository;
    private readonly IAnswerRequestRepository _answerRequestRepository;
    private readonly IMapper _mapper;
    private readonly IUserGrpcClient _userGrpcClient;

    public TestService(IUserGrpcClient userGrpcClient, ITestResultRepository testResultRepository,IMapper mapper, ITestRepository psychologicalTestRepository)
    {
        _testResultRepository = testResultRepository;
        _psychologicalTestRepository = psychologicalTestRepository;
        _mapper = mapper;
        _userGrpcClient = userGrpcClient;
    }

    public async Task PassTestAsync(PassTestDTO dto)
    {
        var answ = _mapper.Map<List<AnswerRequest>>(dto.Answers);

        if (answ == null || !answ.Any())
        {
            throw new ArgumentException("Answers cannot be empty.", nameof(dto.Answers));
        }

        var testHasStudent = new TestResult() { TestId = dto.TestId, UserId = int.Parse(dto.UserId) };
        var added = await _testResultRepository.AddAsync(testHasStudent);
        await _testResultRepository.SaveAsync();
        
        foreach (var entity in answ)
        {
            entity.StudentHasTestId = added.Id;
        }
        await _answerRequestRepository.AddRangeAsync(answ);
        await _answerRequestRepository.SaveAsync();
    }
    
    public async Task<List<TestResultDTO>> GetTestResultsByStudentAsync(int studentId, int pageNumber, int pageSize, CancellationToken token)
    {
        var userReply = await _userGrpcClient.CheckUserAsync(studentId, token);

        if (!userReply.Exists)
        {
            throw new EntityNotFoundException(nameof(studentId));
        }

        if (userReply.Role != Roles.Student)
        {
            throw new WrongRoleForActionRequested(userReply.Role);
        }
        
        var eduMaterials = await _testResultRepository.GetAllAsync(r => r.UserId == studentId, pageNumber, pageSize);
        var eduMaterialsDTOs = _mapper.Map<List<TestResultDTO>>(eduMaterials);
        
        return eduMaterialsDTOs; 
    }
}
