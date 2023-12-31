using FluentValidation;
using FluentValidation.AspNetCore;
using PsychologicalSupportPlatform.Authorization.API.Extensions;
using PsychologicalSupportPlatform.Authorization.API.GrpcServices;
using PsychologicalSupportPlatform.Authorization.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InjectRepos();
builder.Services.InjectServices();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<User>();
builder.Services.AddDatabaseContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAuthorization();
builder.Services.AddAuthenticate(builder.Configuration);
builder.Services.AddSwagger();
builder.Services.AddControllers();
builder.Services.AddGrpc();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddCorsPolicy();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

await app.UseDatabaseSeed();
app.UseCors("AllowAll");
app.MapControllers();
app.MapGrpcService<UserCheckerService>();
app.Run();
