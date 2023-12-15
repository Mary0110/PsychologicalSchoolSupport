using PsychologicalSupportPlatform.Edu.Application.DTOs.Tests;

namespace PsychologicalSupportPlatform.Edu.Application.Interfaces.Tests.Services;

public interface ITestService
{
    Task PassTestAsync(PassTestDTO dto);

    Task<List<TestResultDTO>> GetTestResultsByStudentAsync(int studentId, int pageNumber, int pageSize,
        CancellationToken token);
}
