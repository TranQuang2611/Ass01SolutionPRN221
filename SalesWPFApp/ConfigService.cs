using BusinessObject;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesWPFApp
{
    public class ConfigService
    {
        public static void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton(typeof(IOrderDetailRepository), typeof(OrderDetailRepository));
            services.AddSingleton(typeof(IMemberRepository), typeof(MemberRepository));
            services.AddSingleton(typeof(IOrderRepository), typeof(OrderRepository));
            services.AddSingleton(typeof(IProductRepository), typeof(ProductRepository));
            services.AddSingleton<Login>();
            services.AddSingleton<ProductManagement>();
            services.AddSingleton<MemberManagement>();
            services.AddSingleton<OrderManagement>();
        }
    }
}
