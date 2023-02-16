using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class OrderRepository : IOrderDetailRepository
    {
        private SaleDbContext context;

        public OrderRepository(SaleDbContext context)
        {
            this.context = context;
        }

        // laasy kieu SQL
        public List<Product> GetListOderDetail()
        {
            return this.context.Products.ToList();
        }
    }
}
