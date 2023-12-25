using Mapster;
using PsychologicalSupportPlatform.Edu.Application.DTOs.Tests;
using PsychologicalSupportPlatform.Edu.Domain.Entities;

namespace PsychologicalSupportPlatform.Edu.Application.Mapper;

public class TestMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TestResultDTO, UserTestResult>()
            .TwoWays()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.UserId, src => src.UserId);

        config.NewConfig<AddAnswerDTO, Answer>()
            .TwoWays()
            .Map(dest => dest.AnswerText, src => src.AnswerText);

        config.NewConfig<AddQuestionDTO, Question>()
            .Map(dest => dest.Text, src => src.Text);
        
        config.NewConfig<TestDTO, Test>()
            .TwoWays()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Questions, src => src.Questions);

        config.NewConfig<AddTetsDTO, Test>()
            .TwoWays()
            .Map(dest => dest.Name, src => src.Name);
        
        config.NewConfig<UserAnswerRequestDTO, UserTestResult>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.DatePassed, src => DateTime.Now);

        config.NewConfig<QuestionResultDTO, QuestionResult>()
            .Map(dest => dest.SelectedAnswerId, src => src.AnswerId);
    }
}
    