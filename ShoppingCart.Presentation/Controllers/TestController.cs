using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Presentation.Utilities;

namespace ShoppingCart.Presentation.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Hash(string message)
        {
            return Content(Encryption.Hash(message));
        }


        public IActionResult TestAsymmetric()
        {

            var asykeys = Encryption.GenerateAsymmetricKey();
            var symmkeys = Encryption.GenerateSymmetricKeys();


            MemoryStream msIn = new MemoryStream(symmkeys.Key); msIn.Position = 0;

            MemoryStream encryptedSymmetricKey = Encryption.AsymmetricEncrypt(msIn, asykeys.PublicKey);

            MemoryStream decryptedSymmetricKey = Encryption.AsymmetricDecrypt(encryptedSymmetricKey, asykeys.PrivateKey);

            return Content("success");


        }
    }
}
