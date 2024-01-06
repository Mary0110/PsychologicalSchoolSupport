using Ocelot.Middleware;
using PsychologicalSupportPlatform.OcelotGateway;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureOcelot(builder);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
app.MapControllers();
app.UseRouting();

app.UseSwaggerForOcelotUI(opt =>
{
    opt.PathToSwaggerGenerator = app.Configuration["Ocelot:PathToSwaggerGenerator"];
});

await app.UseOcelot();
app.Run();