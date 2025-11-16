using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using InnerHealth.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure EF Core with SQLite. SQLite is file-based and does not require a local SQL Server installation.
// The connection string is defined in appsettings.json. When using SQLite, EF Core will create the database file
// automatically if it does not exist. This avoids the need for SQL Server Express or LocalDB on the machine.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// API versioning configuration
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

// Adds API Explorer to support Swagger versioning
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// Register AutoMapper for DTO mapping
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Register domain services
builder.Services.AddScoped<InnerHealth.Api.Services.IUserService, InnerHealth.Api.Services.UserService>();
builder.Services.AddScoped<InnerHealth.Api.Services.IWaterService, InnerHealth.Api.Services.WaterService>();
builder.Services.AddScoped<InnerHealth.Api.Services.ISunlightService, InnerHealth.Api.Services.SunlightService>();
builder.Services.AddScoped<InnerHealth.Api.Services.IMeditationService, InnerHealth.Api.Services.MeditationService>();
builder.Services.AddScoped<InnerHealth.Api.Services.ISleepService, InnerHealth.Api.Services.SleepService>();
builder.Services.AddScoped<InnerHealth.Api.Services.IPhysicalActivityService, InnerHealth.Api.Services.PhysicalActivityService>();
builder.Services.AddScoped<InnerHealth.Api.Services.ITaskService, InnerHealth.Api.Services.TaskService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Add a basic Swagger doc for each API version. Runtime groups are added below.
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "InnerHealth API v1",
        Description = "InnerHealth API v1 oferece endpoints para acompanhar hidratação diária, exposição ao sol, meditação, sono, atividade física, tarefas e informações do perfil do usuário."
    });
    options.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2",
        Title = "InnerHealth API v2",
        Description = "InnerHealth API v2 expande a v1 com recomendações automáticas e resumos diários aprimorados."
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// Show developer exception page only in development
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Always enable Swagger and Swagger UI regardless of environment. This ensures the API documentation
// is available in production builds and avoids 404 errors when accessing /swagger.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "InnerHealth API v1");
    c.SwaggerEndpoint("/swagger/v2/swagger.json", "InnerHealth API v2");
});

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();