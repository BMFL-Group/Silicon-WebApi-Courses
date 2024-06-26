using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Infrastructure.Repository;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

// Registering repository services
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<CourseContentRepository>();
builder.Services.AddScoped<CourseIncludesRepository>();
builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<ProgramDetailsRepository>();
builder.Services.AddScoped<SavedCoursesRepository>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<CourseIncludesService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ProgramDetailsService>();
builder.Services.AddScoped<CourseContentService>();

var app = builder.Build();

app.UseSwagger();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
