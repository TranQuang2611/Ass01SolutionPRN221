using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class OrderSearch
    {
        public int OrderId { get; set; }
        public string Email { get; set; }
        public decimal Freight { get; set; }
        public string ProductName { get; set; }
        public DateTime OrderFrom { get; set; }
        public DateTime OrderTo { get; set; }
    }
}
