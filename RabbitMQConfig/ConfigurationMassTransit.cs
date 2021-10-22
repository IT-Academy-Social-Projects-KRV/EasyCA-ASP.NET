using System;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;

namespace RabbitMQConfig
{  
    public class ConfigurationMassTransit
    {
        public Action<IServiceCollectionBusConfigurator> Configurator { get; set; }
        public Action<IBusControl, IServiceProvider> BusControl { get; set; }
        public string ServiceName { get; set; }
    }
}
