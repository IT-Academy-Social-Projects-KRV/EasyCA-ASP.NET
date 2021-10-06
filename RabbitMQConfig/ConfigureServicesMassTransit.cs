using System;
using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RabbitMQConfig
{
    public static class ConfigureServicesMassTransit
    {
        public static void ConfigureServices(IServiceCollection services,
           IConfiguration configuration,
           ConfigurationMassTransit massTransitConfiguration)
        {
            if (massTransitConfiguration == null)
            {
                return;
            }

            var rabbitMQSection = configuration.GetSection("RabbitMQServer");

            if (rabbitMQSection == null)
            {
                throw new Exception("Section is empty");
            }

            var url = rabbitMQSection.GetValue<string>("Url");
            var host = rabbitMQSection.GetValue<string>("Host");

            if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(url))
            {
                throw new Exception("AppSettings does not contains data for RabbitMQ");
            }

            services.AddGenericRequestClient();
            services.AddMassTransit(x =>
            {
                x.AddBus(busFactory =>
                {
                    var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
                    {
                        cfg.Host($"rabbitmq://{url}/{host}", configurator =>
                        {
                            configurator.Username(rabbitMQSection.GetValue<string>("UserName"));
                            configurator.Password(rabbitMQSection.GetValue<string>("Password"));
                        });                      
                        cfg.ConfigureEndpoints(busFactory, SnakeCaseEndpointNameFormatter.Instance);
                    });

                    bus.CreateClientFactory();
                    massTransitConfiguration.BusControl?.Invoke(bus, services.BuildServiceProvider());
                    
                    return bus;
                });
               
                massTransitConfiguration.Configurator?.Invoke(x);
                services.AddMassTransitHostedService();
            });
        }
    }
}
