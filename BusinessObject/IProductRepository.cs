using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public interface IProductRepository
    {
        List<Product> GetAllProduct();
        Product GetProduct(int id);

        List<Product> SearchProduct(Product prod);

        void InsertProduct(Product prod);
    }
}
