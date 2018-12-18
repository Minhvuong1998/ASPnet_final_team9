using Interconnected.Models;
using Interconnected.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Interconnected.Controllers
{
    public class HomeController : BaseController
    {
        CategoriesModels categoriesModels = new CategoriesModels();
        PostsModels postsModels = new PostsModels();
        public ActionResult Index()
        {
            ViewBag.Active = "index";
            ViewBag.ListCate = categoriesModels.GetAllItem();
            ViewBag.ListPost = postsModels.GetItemActive(1, 8, 0);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.Active = "about";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.Active = "contact";

            return View();
        }
    }
}