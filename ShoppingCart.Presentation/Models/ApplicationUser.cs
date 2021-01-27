using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Presentation.Models
{
    public class ApplicationUser: IdentityUser
    {

        public DateTime LastLoggedIn { get; set; }
    }
}
