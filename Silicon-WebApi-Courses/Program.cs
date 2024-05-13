using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;


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

var app = builder.Build();

app.UseSwagger();
app.MapGet("/swagger/v1/swagger.json", async (httpContext) => {
    var swaggerProvider = httpContext.RequestServices.GetRequiredService<ISwaggerProvider>();
    var swagger = swaggerProvider.GetSwagger("v1");
    httpContext.Response.ContentType = "application/json";
    await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(swagger));
});

app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
