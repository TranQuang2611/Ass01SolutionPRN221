using BusinessObject.DTO;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public interface IOrderRepository
    {
        List<Order> ListOrderByMemberId(int memberId);
        List<Order> ListOrderByMemberName(string memberName);

        List<Order> GetAllOrder();

        List<Order> SearchOrder(OrderDTO orderSearch);
    }
}
