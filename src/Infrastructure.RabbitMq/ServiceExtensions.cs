using Core.Domain.RabbitMq.Options;
using Infrastructure.RabbitMq.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.RabbitMq
{
    public static class ServiceExtensions
    {
        public static void AddRabbitMqInfrastructure(this IServiceCollection services)
        {
            services.Configure<RabbitMqConfiguration>(options =>
            {
                options.Hostname = "localhost";
                options.Password = "guest";
                options.QueueName = "CRMQueue";
                options.UserName = "guest";
                options.Port = 5672;
            });
            services.AddScoped<RabbitMqContext, RabbitMqContext>();
        }
    }
}
