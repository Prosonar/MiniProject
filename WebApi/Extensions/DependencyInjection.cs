using Business.Abstract.Service;
using Business.Concrete.Manager;

namespace WebApi.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection DIExtension(this IServiceCollection services)
        {
            services.AddSingleton<IProductService,ProductService>();
            services.AddSingleton<ICategoryService, CategoryService>();
            services.AddSingleton<IProductCategoryService, ProductCategoryService>();


            return services;
        }
    }
}
