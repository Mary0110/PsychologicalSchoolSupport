namespace PsychologicalSupportPlatform.Messaging.Application.DTOs;

public record AddMessageDTO(string SenderId, string ConsumerId, string Text, DateTime DateTime);
