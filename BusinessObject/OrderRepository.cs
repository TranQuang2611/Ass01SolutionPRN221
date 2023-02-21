using AutoMapper.Execution;
using BusinessObject.DTO;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class OrderRepository : IOrderRepository
    {
        public List<Order> GetAllOrder()
        {
            using (var context = new SaleDbContext())
            {
                return context.Orders.Include(x => x.Member).Include(x => x.OrderDetails).ToList();
            }
        }

        public List<Order> ListOrderByMemberId(int memberId)
        {
            using (var context = new SaleDbContext())
            {
                return context.Orders.Where(x => x.MemberId == memberId).Include(x => x.Member).Include(x => x.OrderDetails).ToList();
            }
        }

        public List<Order> ListOrderByMemberName(string memberName)
        {
            using (var context = new SaleDbContext())
            {
                return context.Orders.Where(x => x.Member.Email.Contains(memberName)).ToList();
            }
        }

        public List<Order> SearchOrder(OrderDTO orderSearch)
        {
            List<Order> orders = new List<Order>();
            using (var context = new SaleDbContext())
            {
                orders = context.Orders.Include(x => x.Member).Include(x => x.OrderDetails).ToList();
                if (orderSearch.ID != 0) orders = orders.Where(x => x.OrderId == orderSearch.ID).ToList();
                if(string.IsNullOrEmpty(orderSearch.MemberName)) orders = orders.Where(x => x.Member.Email.ToLower().Contains(orderSearch.MemberName.ToLower())).ToList();
                if(orderSearch.Freight!=0) orders = orders.Where(x => x.Freight >= orderSearch.Freight).ToList();
                return orders;
            }
        }
    }
}
