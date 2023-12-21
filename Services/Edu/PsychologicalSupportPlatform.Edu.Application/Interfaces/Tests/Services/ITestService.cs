using PsychologicalSupportPlatform.Edu.Application.DTOs.Tests;

namespace PsychologicalSupportPlatform.Edu.Application.Interfaces.Tests.Services;

public interface ITestService
{
    Task PassTestAsync(UserAnswerRequestDTO dto);

    Task<List<TestResultDTO>> GetTestResultsByStudentAsync(int studentId, int pageNumber, int pageSize,
        CancellationToken token);

    Task<int> AddTestAsync(AddTetsDTO addProductDto);
}
