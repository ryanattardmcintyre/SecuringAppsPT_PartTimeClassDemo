using Microsoft.AspNetCore.Http;
using ShoppingCart.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Presentation.Models
{
    public class CreateProductModel
    {
        public IFormFile File { get; set; }
        public ProductViewModel Product {get;set;}
    }
}
