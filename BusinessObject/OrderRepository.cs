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
        public void DeleteOrder(int orderId)
        {
            using (var context = new SaleDbContext())
            {
                var orderDetail = context.OrderDetails.Include(x => x.Order).Where(x => x.OrderId == orderId).ToList();
                var order = context.Orders.FirstOrDefault(x => x.OrderId == orderId);
                if (order != null)
                {
                    context.OrderDetails.RemoveRange(orderDetail);
                    context.Orders.Remove(order);
                    context.SaveChanges();
                }
            }
        }

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

        public List<Order> SearchOrder(OrderSearch orderSearch)
        {
            List<Order> orders = new List<Order>();
            using (var context = new SaleDbContext())
            {
                orders = context.Orders.Include(x => x.Member).Include(x => x.OrderDetails).ToList();
                if (orderSearch.OrderId != 0) orders = orders.Where(x => x.OrderId == orderSearch.OrderId).ToList();
                if (!string.IsNullOrEmpty(orderSearch.Email)) orders = orders.Where(x => x.Member.Email.ToLower().Contains(orderSearch.Email.ToLower())).ToList();
                if (orderSearch.Freight != 0) orders = orders.Where(x => x.Freight >= orderSearch.Freight).ToList();
                orders = orders.Where(x => x.OrderDate <= orderSearch.OrderTo && x.OrderDate >= orderSearch.OrderFrom).ToList();
                return orders;
            }
        }
    }
}
