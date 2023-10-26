namespace PsychologicalSupportPlatform.Authorization.Application.Interfaces;

public interface IEncryptionService
{ 
    string HashPassword(string password);
}
