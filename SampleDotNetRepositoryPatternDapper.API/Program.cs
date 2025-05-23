using System.Reflection;
using SampleDotNetRepositoryPatternDapper.App;
using SampleDotNetRepositoryPatternDapper.Core;
using SampleDotNetRepositoryPatternDapper.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
//builder.Services.addSwagger
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "SampleDotNetRepositoryPatternDapper API", Version = "v1" });

    // Add XML comments for better documentation
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SampleDotNetRepositoryPatternDapper API V1");
        c.RoutePrefix = string.Empty; // optional: serve Swagger at root
    });
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
