using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Presentation.Models;

namespace ShoppingCart.Presentation.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        public AdminController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult AllocateRole(string email, string role)
        {

            ApplicationUser originalUserFromDb = _userManager.FindByNameAsync(email).Result;

            if(originalUserFromDb != null)
            _userManager.AddToRoleAsync(originalUserFromDb, role);

            //message allocation was successful
            return View();
        }
    }
}
