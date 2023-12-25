using MapsterMapper;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Common.Errors;
using PsychologicalSupportPlatform.Common.Interfaces;
using PsychologicalSupportPlatform.Edu.Application.DTOs.Tests;
using PsychologicalSupportPlatform.Edu.Application.Interfaces;
using PsychologicalSupportPlatform.Edu.Application.Interfaces.Tests;
using PsychologicalSupportPlatform.Edu.Application.Interfaces.Tests.Repositories;
using PsychologicalSupportPlatform.Edu.Application.Interfaces.Tests.Services;
using PsychologicalSupportPlatform.Edu.Domain.Entities;

namespace PsychologicalSupportPlatform.Edu.Application.Services;

public class TestService: ITestService
{
    private readonly IUserTestResultRepository _userTestResultRepository;
    private readonly ITestRepository _psychologicalTestRepository;
    private readonly IQuestionResultRepository _questionResultRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly IAnswerRepository _answerRepository;
    private readonly IMapper _mapper;
    private readonly IUserGrpcClient _userGrpcClient;
    

    public TestService(IUserGrpcClient userGrpcClient, IUserTestResultRepository userTestResultRepository,IMapper mapper, ITestRepository psychologicalTestRepository, IQuestionRepository questionRepository, IAnswerRepository answerRepository, IQuestionResultRepository questionResultRepository)
    {
        _questionResultRepository = questionResultRepository;
        _answerRepository = answerRepository;
        _questionRepository = questionRepository;
        _userTestResultRepository = userTestResultRepository;
        _psychologicalTestRepository = psychologicalTestRepository;
        _mapper = mapper;
        _userGrpcClient = userGrpcClient;
    }

    public async Task PassTestAsync(UserAnswerRequestDTO dto)
    {
        if (!dto.AnswerRequestDTO.QuestionResultDTOs.Any())
        {
            throw new ArgumentException("Answers cannot be empty.", nameof(dto.AnswerRequestDTO.QuestionResultDTOs));
        }

        var testHasStudent = _mapper.Map<UserTestResult>(dto);
        var added = await _userTestResultRepository.AddAsync(testHasStudent);
        await _userTestResultRepository.SaveAsync();

        var answerRequests = _mapper.Map<List<QuestionResult>>(dto.AnswerRequestDTO.QuestionResultDTOs);
        
        foreach (var entity in answerRequests)
        {
            entity.UserTestResultId = added.Id;
        }
        await _questionResultRepository.AddRangeAsync(answerRequests);
        await _questionResultRepository.SaveAsync();
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
        
        var eduMaterials = await _userTestResultRepository.GetAllAsync(r => r.UserId == studentId, pageNumber, pageSize);
        var eduMaterialsDTOs = _mapper.Map<List<TestResultDTO>>(eduMaterials);
        
        return eduMaterialsDTOs; 
    }

    public async Task<int> AddTestAsync(AddTetsDTO addProductDto)
    {
        if (addProductDto is null)
        {
            throw new WrongRequestDataException(nameof(addProductDto));
        }

        var test = _mapper.Map<Test>(addProductDto);
        var added = await _psychologicalTestRepository.AddAsync(test);
        await _psychologicalTestRepository.SaveAsync();
        var questionList = _mapper.Map<List<Question>>(addProductDto.Questions);

        for(var i = 0; i < questionList.Count; i++)
        {
            var el = questionList[i];
            el.TestId = added.Id;
            var addedQuestion = await _questionRepository.AddAsync(el);
            await _questionRepository.SaveAsync();
            var answerList = _mapper.Map<List<Answer>>(addProductDto.Questions[i].Answers);
            
            foreach (var answer in answerList)
            {
                answer.QuestionId = addedQuestion.Id;
            }

            await _answerRepository.AddRangeAsync(answerList);
            await _answerRepository.SaveAsync();
        }

        return added.Id;
    }
}
