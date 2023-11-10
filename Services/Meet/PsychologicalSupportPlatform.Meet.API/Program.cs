using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MapsterMapper;
using PsychologicalSupportPlatform.Authorization.API.Extensions;
using PsychologicalSupportPlatform.Meet.API.Extensions;
using PsychologicalSupportPlatform.Meet.Application;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Application.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabaseContext(builder.Configuration);
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<OpeningDTO>();
builder.Services.AddControllers();
builder.Services.AddSwagger();
builder.Services.AddAuthenticate(builder.Configuration);
builder.Services.InjectRepos();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(), AssemblyReference.Assembly));
builder.Services.AddSingleton(GetConfiguredMapping.GetConfiguredMappingConfig());
builder.Services.AddScoped<IMapper, ServiceMapper>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();