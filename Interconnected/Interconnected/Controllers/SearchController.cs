using Interconnected.Code;
using Interconnected.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Interconnected.Controllers
{
    public class SearchController : BaseController
    {
        PostsModels postsModels = new PostsModels();
        // GET: Search
        public ActionResult Index(string keySearch, int? page)
        {
            IPagedList ListPost = postsModels.GetItemsSearch(keySearch, page??1, ConstanAppkey.PAGESIZE());
            ViewBag.keySearch = keySearch;
            return View(ListPost);
        }
    }
}