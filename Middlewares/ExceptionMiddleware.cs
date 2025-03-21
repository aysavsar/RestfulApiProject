using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace RestfulApiProject.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Bir sonraki middleware'e geç
                await _next(context);
            }
            catch (Exception ex)
            {
                // Hata yakalandığında buraya düşer
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                // Hata mesajını JSON formatında döndür
                await context.Response.WriteAsync(new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "Sunucu tarafında bir hata oluştu.",
                    Detail = ex.Message // Hata detayı (isteğe bağlı)
                }.ToString());
            }
        }
    }
}