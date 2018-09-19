using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using WindowsService1.Cake;

namespace WindowsService1
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        static void Main()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            serviceCollection.BuildServiceProvider();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var service = serviceProvider.GetService<Service1>();
            service.Run();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<CakeResourcesManager>();
            services.AddTransient<MakingProcedureService>();
            services.AddTransient<Factory>();
            services.AddTransient<Supervisor>();
            services.AddTransient<Service1>();
        }
    }
}
