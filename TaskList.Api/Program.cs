using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskList.Api.Repositories;
using TaskList.Api.Repositories.Interfaces;
using TaskList.Api.Services;
using TaskList.Api.Validation;
using TaskList.Infrastructure;

ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddFilter("Microsoft", LogLevel.Information)
        .AddFilter("System", LogLevel.Information)
        .AddFilter("SampleApp.Program", LogLevel.Information)
        .AddConsole();
});
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddHttpClient<LineNotifyService>();
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
// Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MapperAssembly));
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

string connection = "Server=localhost\\SQLEXPRESS;Database=TaskList;Trusted_Connection=True;TrustServerCertificate=true;";
builder.Services.AddDbContext<TaskListDbContext>(options =>
    {
        options.UseSqlServer(connection,
            sqlOptions =>
            {
                sqlOptions.UseCompatibilityLevel(110);
                sqlOptions.CommandTimeout(30);
                sqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), errorNumbersToAdd: null);
            });
#if DEBUG
        options.EnableSensitiveDataLogging().UseLoggerFactory(loggerFactory);
#endif
    }
);

// Configure CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS
app.UseCors("AllowAll");

//app.UseMiddleware<AuthenMiddleware>();
app.UseFluentValidationHandlerExtension();
app.UseMvc();
app.Run();
