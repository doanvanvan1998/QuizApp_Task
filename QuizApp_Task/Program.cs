using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuizApp_Task.Data;
using QuizApp_Task.Data;
using QuizApp_Task.Entities;
using QuizApp_Task.Repository;
using QuizApp_Task.Repository.impl;
using QuizApp_Task.Service;
using QuizApp_Task.Service.impl;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext with MySQL
builder.Services.AddDbContext<QuizAppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("ConnStr"),
    new MySqlServerVersion(new Version(10, 6, 16))
));

// For Repository
builder.Services.AddScoped<IQuizRepository, QuizRepositoryImpl>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepositoryImpl>();
builder.Services.AddScoped<IAnswerRepository, AnswerRepositoryImpl>();

// For Service
builder.Services.AddScoped<IQuizService, QuizServiceImpl>();
builder.Services.AddScoped<IQuestionService, QuestionServiceImpl>();
builder.Services.AddScoped<IAnswerService, AnswerServiceImpl>();

// Add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add Identity
builder.Services.AddIdentity<UserEntity, RoleEntity>()
    .AddEntityFrameworkStores<QuizAppDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Ensure the use of authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
