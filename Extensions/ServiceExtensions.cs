using Microsoft.Extensions.DependencyInjection;
using RestfulApiProject.Services;
using RestfulApiProject.Services.Interfaces;

namespace RestfulApiProject.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Uygulama servislerini bağımlılık enjeksiyon (DI) konteynerine ekleyen genişletme metodudur.
        /// </summary>
        /// <param name="services">IServiceCollection örneği</param>
        public static void ConfigureServices(this IServiceCollection services)
        {
            // ProductService servisini Scoped (istek bazlı) yaşam süresiyle kaydediyoruz.
            services.AddScoped<IProductService, ProductService>();
        }
    }
}
