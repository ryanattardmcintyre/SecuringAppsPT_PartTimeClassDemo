using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShoppingCart.Application.ViewModels
{
    public class ProductViewModel
    {
     
        public Guid Id { get; set; }

        

        [Required(AllowEmptyStrings =false, ErrorMessage ="Name is required")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage ="Name is not valid. Use only letters")]
        public string Name { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Pricing is required")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        public int CategoryId { get; set; }

        public CategoryViewModel Category { get; set; }

        public string ImageUrl { get; set; }

        public int Stock { get; set; }
        //public List<CategoryViewModel> Categories { get; set; }
        public string Owner { get; set; }


    }
}
