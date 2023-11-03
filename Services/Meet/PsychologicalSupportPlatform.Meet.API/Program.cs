using System.Reflection;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using PsychologicalSupportPlatform.Authorization.API.Extensions;
using PsychologicalSupportPlatform.Meet.API.Extensions;
using PsychologicalSupportPlatform.Meet.Domain.Interfaces;
using PsychologicalSupportPlatform.Meet.Infrastructure.Data;
using PsychologicalSupportPlatform.Meet.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly(builder.Configuration.GetSection("MigrationsAssembly").Get<string>())));

builder.Services.AddControllers();
builder.Services.AddSwagger();
builder.Services.AddAuthenticate(builder.Configuration);
builder.Services.AddScoped(typeof(IMeetupRepository), typeof(MeetupRepository));
builder.Services.AddScoped(typeof(IOpeningRepository), typeof(OpeningRepository));
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

app.MapControllers();

app.Run();