using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingCart.Presentation.Models;

namespace ShoppingCart.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
        //    _logger.LogInformation("User just accessed Index method");
        //    try
        //    {
        //        throw new Exception("thrown on purpose");
        //    }
        //    catch (Exception ex)
        //    {
        //        try
        //        {
        //            _logger.LogError(ex.Message);
        //        }
        //        catch { }

        //       return  RedirectToAction("ShowError", new { message = "Error occurred" });
        //    }
            


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult ShowError(string message)
        {
            TempData["error"] = message;
            return View();
        }

        [HttpGet]
        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ContactUs(string email, string message)
        {
            
            //.....
            return Ok();

        }
    }
}
