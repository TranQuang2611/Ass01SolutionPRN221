using BusinessObject;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigService.ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        //private void ConfigureServices(ServiceCollection services)
        //{
        //    services.AddSingleton(typeof(IOrderDetailRepository), typeof(OrderDetailRepository));
        //    services.AddSingleton(typeof(IMemberRepository), typeof(MemberRepository));
        //    services.AddSingleton(typeof(IOrderRepository), typeof(OrderRepository));
        //    services.AddSingleton(typeof(IProductRepository), typeof(ProductRepository));
        //    services.AddSingleton<Login>();
        //    services.AddSingleton<ProductManagement>();
        //}

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var loginWindow = serviceProvider.GetService<Login>();
            loginWindow.Show();
        }

    }
}
