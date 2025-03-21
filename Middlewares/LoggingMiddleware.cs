using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RestfulApiProject.Middlewares
{
    /// <summary>
    /// Gelen HTTP isteklerini loglayan ara katman (middleware) sınıfı.
    /// </summary>
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Middleware'in bağımlılıklarını alarak örneğini oluşturur.
        /// </summary>
        /// <param name="next">Sonraki middleware bileşeni</param>
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// HTTP isteği işlendiğinde çağrılan metot.
        /// </summary>
        /// <param name="context">HTTP istek bağlamı</param>
        public async Task InvokeAsync(HttpContext context)
        {
            // İstek yapılan endpoint'in yolunu debug penceresine yazdırıyoruz.
            Debug.WriteLine($"Action'a girildi: {context.Request.Path}");

            // Bir sonraki middleware'e geçiş yapıyoruz.
            await _next(context);
        }
    }
}
