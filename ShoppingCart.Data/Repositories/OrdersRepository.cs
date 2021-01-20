using ShoppingCart.Data.Context;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Data.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        ShoppingCartDbContext _context;
        public OrdersRepository(ShoppingCartDbContext context)
        {
            _context = context;
        }

        public void AddOrder(Order order)
        {
          //  _context.Orders.Add(order);
         //   _context.SaveChanges();
        }

        public void AddOrderDetail(OrderDetail orderDetail)
        {

            //  _context.OrdersDetails.Add(orderDetail);
            //   _context.SaveChanges();
        }
    }
}
