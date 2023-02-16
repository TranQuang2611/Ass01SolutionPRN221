using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        // lay kieu mysql
        public List<Product> GetListOderDetail()
        {
            using(var context = new SaleDbContext())
            {
                return context.Products.ToList();
            }
        }

        public List<OrderDetail> GetListOrderDetailByOrderId(int orderId)
        {
            using (var context = new SaleDbContext())
            {
                return context.OrderDetails.Where(x => x.OrderId == orderId).ToList();
            }
        }
    }
}
