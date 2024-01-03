using PsychologicalSupportPlatform.Edu.Application.DTOs.Tests;

namespace PsychologicalSupportPlatform.Edu.Application.Interfaces.Tests.Services;

public interface ITestResultService
{
    Task GetTestResultByStudent(int studentId, int testId, TestResultDTO dto);
}
