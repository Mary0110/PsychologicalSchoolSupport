using MapsterMapper;
using PsychologicalSupportPlatform.Edu.API.Extensions;
using PsychologicalSupportPlatform.Report.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.InjectRepositories();
builder.Services.AddControllers();
builder.Services.AddGrpc();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddAuthenticate(builder.Configuration);
builder.Services.AddSingleton(GetConfiguredMapping.GetConfiguredMappingConfig());
builder.Services.AddScoped<IMapper, ServiceMapper>();
builder.Services.AddSwagger();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDatabaseContext(builder.Configuration);
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.ConfigureMinio(builder.Configuration);
builder.Services.AddElasticSearch(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();