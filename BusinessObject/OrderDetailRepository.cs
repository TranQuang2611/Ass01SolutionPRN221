using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
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
                return context.OrderDetails.Include(x => x.Order).Include(x => x.Product).Where(x => x.OrderId == orderId).ToList();
            }
        }
    }
}
