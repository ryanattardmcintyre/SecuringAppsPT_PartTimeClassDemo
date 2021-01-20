using ShoppingCart.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.Interfaces
{
   public interface IOrdersService
    {
        void Checkout(
           List<ProductViewModel> products,
           string email
           );
    }
}
