using Mapster;
using PsychologicalSupportPlatform.Edu.Application.DTOs;
using PsychologicalSupportPlatform.Edu.Application.DTOs.Tests;
using PsychologicalSupportPlatform.Edu.Domain.Entities;
using PsychologicalSupportPlatform.Edu.Domain.Entities.Tests;

namespace PsychologicalSupportPlatform.Edu.Application.Mapper;

public class TestMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TestResultDTO, TestResult>()
            .TwoWays()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Test, src => src.Test)
            .Map(dest => dest.UserId, src => src.UserId);

        config.NewConfig<AnswerDTO, Answer>()
            .TwoWays()
            .Map(dest => dest.AnswerText, src => src.AnswerText)
            .Map(dest => dest.IsCorrect, src => src.IsCorrect);

        config.NewConfig<QuestionDTO, Question>()
            .TwoWays()
            .Map(dest => dest.Text, src => src.Text)
            .Map(dest => dest.Answers, src => src.Answers);

        config.NewConfig<TestDTO, Test>()
            .TwoWays()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Questions, src => src.Questions);
        
        config.NewConfig<PassTestDTO, TestResult>()
            .TwoWays()
            .Map(dest => dest.Answers, src => src.Answers)
            .Map(dest => dest.TestId, src => src.TestId)
            .Map(dest => dest.UserId, src => src.UserId);

        config.NewConfig<PassTestDTO, TestResult>()
            .TwoWays()
            .Map(dest => dest.Answers, src => src.Answers)
            .Map(dest => dest.TestId, src => src.TestId)
            .Map(dest => dest.UserId, src => src.UserId);
    }
}
    