using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Presentation.ActionFilters;
using ShoppingCart.Presentation.Models;
using ShoppingCart.Presentation.Utilities;

namespace ShoppingCart.Presentation.Controllers
{
   
    public class ProductsController : Controller
    {
        private IProductsService _prodService;
        private IWebHostEnvironment _host;
        private ILogger<ProductsController> _logger;
        public ProductsController(IProductsService productsService, IWebHostEnvironment host, ILogger<ProductsController> logger)
        {
            _prodService = productsService;
            _host = host;
            _logger = logger;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            _logger.LogInformation("User just accessed Products/Create method");
            try
            {
                throw new Exception("thrown on purpose");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken()]
        public IActionResult Create(CreateProductModel model)
        {
                if (ModelState.IsValid)
                {

                    if (model.File != null)
                    {
                        //check whether the file is an image
                        //FF D8 = 255 216

                        Stream file = model.File.OpenReadStream();
                        int firstByte = file.ReadByte();
                        int secondByte = file.ReadByte(); //position moved to 2

                        if (firstByte == 255 && secondByte == 216 && Path.GetExtension(model.File.FileName) == ".jpg"
                            && file.Length < 4194304
                            )
                        {
                            //continue to uploading the file
                            string absolutePath = _host.WebRootPath + @"\images\";
                            string uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(model.File.FileName);

                            file.Position = 0;

                            MemoryStream ms = new MemoryStream();
                            file.CopyTo(ms);
                            ms.Position = 0;


                            System.IO.File.WriteAllBytes(absolutePath + uniqueName, ms.ToArray());

                            model.Product.ImageUrl = @"\images\" + uniqueName;
                            model.Product.Description = HtmlEncoder.Default.Encode(model.Product.Description);
                            model.Product.Name = HtmlEncoder.Default.Encode(model.Product.Name);
                            model.Product.CategoryId = 1;
                            _prodService.AddProduct(model.Product);
                            TempData["info"] = "File   accepted";
                        }
                        else
                        {
                            TempData["warning"] = "File not accepted";
                        }

                    }
                    else
                        ModelState.AddModelError(string.Empty, "No file was uploaded");
                }
                else
                {
                    TempData["warning"] = "Check your inputs";

                    ModelState.AddModelError(string.Empty, "Check your inputs");
                }
          

            return View();
        }


        [Authorize]
        public IActionResult Index()
        {
            var list = _prodService.GetProducts().ToList();


            return View(list);
        }

        public IActionResult Details(string id)
        {
            //decryption must take place
            id = id.Replace("|", "/").Replace("_", "+").Replace("$", "=");
            string output = Encryption.SymmetricDecrypt(id);

            Guid productId = new Guid(output);

            var p = _prodService.GetProduct(productId);
            return View(p);
        }


        [Authorize]
        [HttpGet][Ownership]
        public IActionResult Edit(Guid id)
        {
            
            var p = _prodService.GetProduct(id);
            return View(p);
        }

        [HttpPost]
        [Authorize]
        [Ownership]
        public IActionResult Edit(Guid id, ProductViewModel model)
        {
            

            //call _prodService.UpdateProduct(model);
            return View(model);
        }
    }
}
