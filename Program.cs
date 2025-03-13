using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Controller'ları servis olarak ekle
builder.Services.AddControllers();

var app = builder.Build();

// Geliştirme ortamında ayrıntılı hata mesajları göster
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    // Production ortamında global hata yönetimi
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Sunucu tarafında bir hata oluştu."
            }.ToString());
        });
    });
}

// Yetkilendirme middleware'ini ekle
app.UseAuthorization();

// Controller endpoint'lerini map'le
app.MapControllers();

// Uygulamayı çalıştır
app.Run();