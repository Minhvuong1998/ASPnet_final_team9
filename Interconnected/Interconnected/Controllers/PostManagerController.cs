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
    [Authorize]
    public class PostManagerController : BaseController
    {
        PostsModels postsModels = new PostsModels();
        CategoriesModels categoriesModels = new CategoriesModels();
        CheckAuth Check = new CheckAuth();
        UserModels userModels = new UserModels();
        // GET: PostManager
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

            CustomPrincipal prin = (CustomPrincipal)User;
            IPagedList ListPost = null;
            if (prin.ROLE.Equals(ConstanAppkey.ADMIN()))
            {
                ListPost = postsModels.GetItems(page ?? 1, ConstanAppkey.PAGESIZE());
            }
            else if (prin.ROLE.Equals(ConstanAppkey.MOD()))
            {
                ListPost = postsModels.GetItemsMod(page ?? 1, ConstanAppkey.PAGESIZE(), prin);
            }
            else
            {
                ListPost = postsModels.GetItems(page ?? 1, ConstanAppkey.PAGESIZE(), prin);
            }
            ViewBag.Active = "manager";
            return View(ListPost);
        }

        public ActionResult Add()
        {
            ViewBag.ListCategories = categoriesModels.GetAllItem();
            ViewBag.Active = "manager";
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Add(POST Post, FormCollection form, HttpPostedFileBase PICTURE)
        {
            CustomPrincipal prin = (CustomPrincipal)User;
            if (ModelState.IsValid)
            {
                //upload file
                if (PICTURE != null && PICTURE.ContentLength > 0)
                {
                    var path = Server.MapPath("~/Assets/Upload/Post/");
                    ImgUpload imgUpload = new ImgUpload();
                    Post.PICTURE = imgUpload.Upload(PICTURE, path);
                }
                if (prin.ROLE.Equals(ConstanAppkey.ADMIN()))
                {
                    if (this.Request.Form["ACTIVE"] != null)
                    {
                        Post.ACTIVE = true;
                    }
                    else
                    {
                        Post.ACTIVE = false;
                    }
                }
                else
                {
                    Post.ACTIVE = false;
                }
                Post.ID_USER = prin.ID;
                int idPosst = postsModels.AddItem(Post);
                if (idPosst > 0)
                {
                    Session["mes"] = "Thêm thành công";
                }
                else
                {
                    var dataFile = Server.MapPath("~/Assets/Upload/Post/" + Post.PICTURE);
                    ImgUpload imgUpload = new ImgUpload();
                    imgUpload.Delete(dataFile);
                    Session["mes_er"] = "Thêm thất bại";
                }
                return RedirectToAction("Index", "PostManager");
            }

            ViewBag.ListCategories = categoriesModels.GetAllItem();
            return View(Post);
        }

        public ActionResult Edit(int id)
        {
            POST Post = postsModels.GetItem(id);
            CustomPrincipal prin = (CustomPrincipal)User;
            if (Check.checkUserEdit(userModels.GetItem(Post.ID_USER)))
            {
                ViewBag.ListCategories = categoriesModels.GetAllItem();
                ViewBag.Active = "manager";
                return View(Post);
            }
            Session["mes_er"] = "Bạn không đủ quyền";
            return RedirectToAction("Index", "PostManager");
        }
        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Edit(int id, POST Post, FormCollection form, HttpPostedFileBase PICTURE)
        {
            CustomPrincipal prin = (CustomPrincipal)User;
            if (ModelState.IsValid)
            {
                POST PostEdit = postsModels.GetItem(id);
                string picture_old = "";
                string picture_new = "";
                //upload file
                if (PICTURE != null && PICTURE.ContentLength > 0)
                {
                    var path = Server.MapPath("~/Assets/Upload/Post/");
                    ImgUpload imgUpload = new ImgUpload();
                    Post.PICTURE = imgUpload.Upload(PICTURE, path);
                    picture_new = Post.PICTURE;
                    picture_old = PostEdit.PICTURE;
                }
                else
                {
                    Post.PICTURE = PostEdit.PICTURE;
                }
                if (this.Request.Form["ACTIVE"] != null)
                {
                    Post.ACTIVE = true;
                }
                else
                {
                    if (prin.ROLE.Equals(ConstanAppkey.ADMIN()) || prin.ROLE.Equals(ConstanAppkey.MOD()))
                    {
                        Post.ACTIVE = false;
                    }
                    else
                    {
                        Post.ACTIVE = PostEdit.ACTIVE;
                    }
                }
                int idPosst = postsModels.EditItem(id, Post);
                if (idPosst > 0)
                {
                    if (!picture_old.Equals(""))
                    {
                        var dataFile = Server.MapPath("~/Assets/Upload/Post/" + picture_old);
                        ImgUpload imgUpload = new ImgUpload();
                        imgUpload.Delete(dataFile);
                    }
                    Session["mes"] = "Sửa thành công";
                }
                else
                {
                    if (!picture_new.Equals(""))
                    {
                        var dataFile = Server.MapPath("~/Assets/Upload/Post/" + picture_new);
                        ImgUpload imgUpload = new ImgUpload();
                        imgUpload.Delete(dataFile);
                    }
                    Session["mes_er"] = "Sửa thất bại";
                }
                return RedirectToAction("Index", "PostManager");
            }
            ViewBag.ListCategories = categoriesModels.GetAllItem();
            return View(Post);
        }
        public ActionResult Delete(int id)
        {
            POST PostDelete = postsModels.GetItem(id);
            if (Check.checkPostDelete(PostDelete))
            {
                string pictureOld = PostDelete.PICTURE;
                if (postsModels.DeleteItem(id) > 0)
                {
                    var dataFile = Server.MapPath("~/Assets/Upload/Post/" + pictureOld);
                    ImgUpload imgUpload = new ImgUpload();
                    imgUpload.Delete(dataFile);
                    Session["mes"] = "Xóa thành công!!!";
                }
                else
                {
                    Session["mes_er"] = "Xóa thất bại";
                }
            }
            else
            {
                Session["mes_er"] = "Xóa thất bại! không đủ quyền";
            }
            return RedirectToAction("Index", "PostManager");
        }
    }
}