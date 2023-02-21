using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class OrderDTO
    {
        public int ID { get; set; }
        public string MemberName { get; set; }
        public string OrderDate { get; set; }
        public string RequiredDate { get; set; }
        public string ShippedDate { get; set;}
        public decimal Freight { get; set; }

        public OrderDTO()
        {
        }
    }
}
