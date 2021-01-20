using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace ShoppingCart.Application.Services
{
    public class OrdersService : IOrdersService
    {
        private IOrdersRepository _ordersRepo;
        private IProductsRepository _productsRepo;
        public OrdersService(IOrdersRepository ordersRepo,
            IProductsRepository productsRepo)
        {
            _ordersRepo = ordersRepo;
            _productsRepo = productsRepo;
        }


        public void Checkout(List<ProductViewModel> products, string email)
        {
            //1. Check qty from the stock
            //2. Create order
            //3. Create order details
            //4. deduct qty from the stock

            using (TransactionScope ts = new TransactionScope())
            {

                foreach (var p in products)
                {
                    Product product = _productsRepo.GetProduct(p.Id);
                    if (product.Stock >= p.Qty)
                    {
                    }
                    else
                    {
                        throw new Exception("Qty bigger than stock for...");
                    }
                }

                Order myOrder = new Order();
                myOrder.Id = Guid.NewGuid();
                myOrder.DatePlaced = DateTime.Now;
                myOrder.Email = email;

                _ordersRepo.AddOrder(myOrder);

                foreach (var p in products)
                {
                    OrderDetail detail = new OrderDetail()
                    {
                        OrderFk = myOrder.Id,
                        ProductFK = p.Id
                        //Quantity
                    };

                    _ordersRepo.AddOrderDetail(detail);
                }

                ts.Complete();
            }


        }
    }
}
