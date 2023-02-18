using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class ProductDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Category { get; set; }
        public string WeightOfProduct { get; set; }
        public decimal Price { get; set; }
        public int Instock { get; set; }

        public ProductDTO()
        {
        }
    }
}
