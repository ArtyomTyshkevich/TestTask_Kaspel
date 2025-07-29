using TestTask.Infrastructure.DI;
using TestTask.Application.Interfaces.Repositories.UnitOfWork;
using TestTask.Infrastructure.Repositories.UnitOfWork;
using TestTask.Application.Interfaces.Services;
using TestTask.Infrastructure.Services;
using TestTask.Application.Mapping;
using FluentValidation.AspNetCore;

namespace Chat.WebAPI.DI
{
    public static class BusinessLogicRegister
    {
        public static void AddBusinessLogic(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureDatabase(configuration);
            services.AddScoped<IUnitOfWork, UnitOfWork>();         
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddAutoMapper(typeof(OrderMappingProfile).Assembly);
            services.ConfigureCors(configuration);
            services.AddFluentValidationAutoValidation();
        }
    }
}