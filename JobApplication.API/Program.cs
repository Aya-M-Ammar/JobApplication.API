
using AutoMapper;

using JobApplication.Application.ImplimentationContract;
using JobApplication.Application.Mediator.Command.CancelJob;
using JobApplication.Application.Services.Mapper;
using JobApplication.Application.Services.ServiceAbstraction;
using JobApplication.Application.Services.ServiceImplimentation;
using JobApplication.Domain;
using JobApplication.Infrastracture.Contract;
using JobApplication.Infrastracture.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<JobDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(
               typeof(IGenericRepository<>),
               typeof(GenericRepository<>));
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappProfile>();
});
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CancelJobHandler).Assembly));


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
