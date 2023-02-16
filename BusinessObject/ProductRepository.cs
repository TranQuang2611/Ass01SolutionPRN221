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
        public List<Product> GetAllProduct()
        {
            using (var context = new SaleDbContext())
            {
                return context.Products.ToList();
            }
        }

        public Product GetProduct(int id)
        {
            using (var context = new SaleDbContext())
            {
                return context.Products.FirstOrDefault(prod => prod.ProductId == id);
            }        
        }

        public void InsertProduct(Product prod)
        {
            Product newProd = new Product
            {
                ProductId = prod.ProductId,
                ProductName= prod.ProductName,
                CategoryId = prod.CategoryId,
                Weight = prod.Weight,
                UnitPrice = prod.UnitPrice,
                UnitsInStock = prod.UnitsInStock
            };
            using (var context = new SaleDbContext())
            {
                context.Products.Add(newProd);
                context.SaveChanges();
            }
        }

        public List<Product> SearchProduct(Product prod)
        {
            List<Product> products = new List<Product>();
            using (var context = new SaleDbContext())
            {
                products = context.Products.ToList();
                if (prod.ProductId != 0) products = products.Where(x => x.ProductId== prod.ProductId).ToList();
                if (prod.CategoryId != 0) products = products.Where(x => x.CategoryId == prod.CategoryId).ToList();
                if (!string.IsNullOrEmpty(prod.ProductName)) products = products.Where(x => x.ProductName.Contains(prod.ProductName)).ToList();
                if(!string.IsNullOrEmpty(prod.Weight)) products = products.Where(x => x.Weight.Contains(prod.Weight)).ToList();
                if(prod.UnitPrice != 0) products = products.Where(x => x.UnitPrice >= prod.UnitPrice).ToList();
                if (prod.UnitsInStock != 0) products = products.Where(x => x.UnitsInStock >= prod.UnitsInStock).ToList();
                return products;
            }
        }
    }
}
