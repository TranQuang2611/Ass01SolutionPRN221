using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class OrderRepository : IOrderRepository
    {
        public List<Order> ListOrderByMemberId(int memberId)
        {
            using (var context = new SaleDbContext())
            {
                return context.Orders.Where(x => x.MemberId == memberId).ToList();
            }
        }

        public List<Order> ListOrderByMemberName(string memberName)
        {
            using (var context = new SaleDbContext())
            {
                return context.Orders.Where(x => x.Member.Email.Contains(memberName)).ToList();
            }
        }
    }
}
