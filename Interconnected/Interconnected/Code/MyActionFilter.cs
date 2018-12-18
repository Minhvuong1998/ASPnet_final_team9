using Interconnected.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Interconnected.Code
{
    public class MyActionFilter : ActionFilterAttribute
    {
        CategoriesModels categoriesModels = new CategoriesModels();
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // if the ActionResult is not a ViewResult (e.g JsonResult, ContentResult),
            // there is no ViewData so don't do anything.
            var viewResult = filterContext.Result as ViewResult;

            if (viewResult != null)
            {
                // call your function, do whatever you want to the result, e.g:
                viewResult.ViewData["ListCategories"] = categoriesModels.GetAllItem();
            }
        }
    }
}