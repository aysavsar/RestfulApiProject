using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RestfulApiProject.Extensions;
using RestfulApiProject.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Controller'ları servis olarak ekle
builder.Services.AddControllers();

// Swagger'ı ekle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection'ı ekle
builder.Services.ConfigureServices();

var app = builder.Build();

// Geliştirme ortamında Swagger'ı kullan
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestfulApiProject v1");
    });
}

// Loglama middleware'ini ekle
app.UseMiddleware<LoggingMiddleware>();

// Global hata yönetimi middleware'ini ekle
app.UseMiddleware<ExceptionMiddleware>();

// Yetkilendirme middleware'ini ekle
app.UseAuthorization();

// Controller endpoint'lerini map'le
app.MapControllers();

// Uygulamayı çalıştır
app.Run();