using Microsoft.AspNetCore.Mvc.Filters;
using ShoppingCart.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingCart.Presentation.ActionFilters
{
    public class OwnershipAttribute: ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var id = context.ActionArguments["id"].ToString();

            //httpcontext has many information >> initialized injected instances & User.identity.name
            IProductsService _prodService = context.HttpContext.RequestServices.GetService<IProductsService>();
            var owner = _prodService.GetProduct(new Guid(id)).Owner;

            if (owner != context.HttpContext.User.Identity.Name)
            {
                context.Result = new UnauthorizedObjectResult("Access Denied");
            }
            else
            {
                base.OnActionExecuting(context);
            }
            
        }

    }
}
