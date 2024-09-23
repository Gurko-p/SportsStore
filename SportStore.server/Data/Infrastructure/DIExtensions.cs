using SportStore.server.Data.Interfaces;
using SportStore.server.Data.Models;
using SportStore.server.Data.Repositories;

namespace SportStore.server.Data.Infrastructure
{
    public static class DIExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Category>, CategoryRepository>();
            services.AddScoped<IRepository<Cart>, CartRepository>();
            services.AddScoped<IRepository<Order>, OrderRepository>();
            services.AddScoped<IRepository<Product>, ProductRepository>();
            services.AddScoped<DataManager>();
            return services;
        }
    }
}
