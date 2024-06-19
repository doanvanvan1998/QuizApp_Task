using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizApp_Task.Auth;
using QuizApp_Task.Repository;
using QuizApp_Task.Repository.impl;
using QuizApp_Task.Service;
using QuizApp_Task.Service.impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("ConnStr"),
        new MySqlServerVersion(new Version(10, 6, 16))
    ));

// For Repository
builder.Services.AddScoped<IQuizRepository, QuizRepositoryImpl>();

// For Service
builder.Services.AddScoped<IQuizService, QuizServiceImpl>();

// mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
