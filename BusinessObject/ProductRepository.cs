using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class ProductRepository : IProductRepository
    {
        SaleDbContext context;

        public ProductRepository(SaleDbContext context)
        {
            this.context = context;
        }

        public Product GetProduct(int id)
        {
            return this.context.Products.FirstOrDefault(prod => prod.ProductId== id);
        }
    }
}
