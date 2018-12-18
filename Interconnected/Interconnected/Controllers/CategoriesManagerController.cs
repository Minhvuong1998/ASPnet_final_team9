using Interconnected.Code;
using Interconnected.Code.CustomAuth;
using Interconnected.Models;
using Interconnected.Models.Entity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Interconnected.Controllers
{
    [CustomAuthorize(Role = "mod")]
    public class CategoriesManagerController : BaseController
    {
        CategoriesModels categoriesModels = new CategoriesModels();
        // GET: Categories
        public ActionResult Index(int? page)
        {
            // Show message
            if (Session["mes"] != null)
            {
                ViewBag.message = Session["mes"];
                Session.Remove("mes");
            }
            if (Session["mes_er"] != null)
            {
                ViewBag.message_er = Session["mes_er"];
                Session.Remove("mes_er");
            }

            IPagedList ListCategories = categoriesModels.GetItems(page ?? 1, ConstanAppkey.PAGESIZE());
            return View(ListCategories);
        }

        public ActionResult Add()
        {
            ViewBag.ListCategories = categoriesModels.GetAllItem();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Add(CATEGORy Category)
        {
            if (ModelState.IsValid)
            {
                int idCate = categoriesModels.AddItem(Category);
                if (idCate > 0)
                {
                    Session["mes"] = "Thêm thành công";
                }
                else
                {
                    Session["mes_er"] = "Thêm thất bại";
                }
                return RedirectToAction("Index", "CategoriesManager");
            }

            ViewBag.ListCategories = categoriesModels.GetAllItem();
            return View(Category);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ListCategories = categoriesModels.GetAllItem();
            CATEGORy Category = categoriesModels.GetItem(id);
            return View(Category);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CATEGORy Category)
        {
            if (ModelState.IsValid)
            {
                int idCate = categoriesModels.EditItem(id, Category);
                if (idCate > 0)
                {
                    Session["mes"] = "Sửa thành công";
                }
                else
                {
                    Session["mes_er"] = "Sửa thất bại";
                }
                return RedirectToAction("Index", "CategoriesManager");
            }

            ViewBag.ListCategories = categoriesModels.GetAllItem();
            return View(Category);
        }

        public ActionResult Delete(int id)
        {
            int i = categoriesModels.DeleteItem(id);
            if (i > 0)
            {
                Session["mes"] = "Xóa thành công!!!";
            }
            else if(i==-1)
            {
                Session["mes_er"] = "Xóa thất bại (Xóa bài viết thuộc chủ đề này trước, hoặc chủ đề con của chủ đề này)";
            }else{
                Session["mes_er"] = "Xóa thất bại";
            }
            return RedirectToAction("Index", "CategoriesManager");
        }
    }
}