using Internship_Aleksa.Data;
using Internship_Aleksa.Domain.Interfaces;
using Internship_Aleksa.Data.Repositories;
using Internship_Aleksa.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Internship_Aleksa.Data.FileStorage;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Internship API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
            new OpenApiSecurityScheme{
                Reference = new OpenApiReference{
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});


builder.Services.Configure<FileStorageOptions>(
    builder.Configuration.GetSection("Storage"));

var useFileStorage = builder.Configuration.GetSection("Storage").GetValue<bool>("UseFileStorage");

if (useFileStorage)
{
    builder.Services.AddSingleton<FileStorageOptions>(
        sp => sp.GetRequiredService<IOptions<FileStorageOptions>>().Value);

    builder.Services.AddSingleton<IStudentRepository, StudentFileRepository>();
}
else
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddScoped<IStudentRepository, StudentRepository>();
}

builder.Services.AddScoped<StudentService>();

builder.Services.AddSingleton<CourseFileStorage>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Internship API v1");
    c.RoutePrefix = ""; // Swagger se otvara na root-u: https://localhost:51775/
});


app.MapControllers();

app.Run();
