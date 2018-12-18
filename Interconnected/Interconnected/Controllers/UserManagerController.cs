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
    public class UserManagerController : BaseController
    {
        UserModels userModels = new UserModels();
        RoleModels roleModels = new RoleModels();
        CheckAuth Check = new CheckAuth();
        // GET: UserManager
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

            ViewBag.Active = "manager";
            CustomPrincipal prin = (CustomPrincipal)User;
            IPagedList ListUser = null;
            if (prin.ROLE.Equals(ConstanAppkey.ADMIN()))
            {
                ListUser = userModels.GetItems(page ?? 1, ConstanAppkey.PAGESIZE());
            }
            else if (prin.ROLE.Equals(ConstanAppkey.MOD()))
            {
                ListUser = userModels.GetItemsMod(page ?? 1, ConstanAppkey.PAGESIZE(), prin);
            }
            return View(ListUser);
        }

        public ActionResult Add()
        {
            if (Session["mes_er"] != null)
            {
                ViewBag.message_er = Session["mes_er"];
                Session.Remove("mes_er");
            }

            ViewBag.Active = "manager";
            ViewBag.ListRole = roleModels.GetAllItems();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Add(USER UserR, FormCollection form, HttpPostedFileBase Picture)
        {
            if (Check.checkUserAdd(UserR))
            {
                if (ModelState.IsValid || UserR.PASSWORD != null || !UserR.PASSWORD.Equals(""))
                {
                    if (userModels.CheckEmail(UserR.EMAIL))
                    {
                        //upload file
                        if (Picture != null && Picture.ContentLength > 0)
                        {
                            var path = Server.MapPath("~/Assets/Upload/User/");
                            ImgUpload imgUpload = new ImgUpload();
                            UserR.PICTURE = imgUpload.Upload(Picture, path);
                        }
                        if (this.Request.Form["ACTIVE"] != null)
                        {
                            UserR.ACTIVE = true;
                        }
                        else
                        {
                            UserR.ACTIVE = false;
                        }
                        if (userModels.AddItem(UserR) > 0)
                        {
                            Session["mes"] = "Thêm thành công!!!";
                        }
                        else
                        {
                            Session["mes_er"] = "Thêm thất bại!!!";
                        }
                        return RedirectToAction("Index", "UserManager");
                    }
                    else
                    {
                        Session["mes_er"] = "Email đã đăng ký!!!";
                    }
                }
            }
            ViewBag.ListRole = roleModels.GetAllItems();
            return View(UserR);
        }

        public ActionResult Edit(int id)
        {
            USER User = userModels.GetItem(id);
            if (Check.checkUserEdit(User))
            {
                ViewBag.Active = "manager";
                ViewBag.ListRole = roleModels.GetAllItems();
                return View(User);
            }
            return RedirectToAction("NotFound", "Error");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(int id, USER UserE, FormCollection form, HttpPostedFileBase Picture)
        {
            CustomPrincipal prin = (CustomPrincipal)User;
            if (userModels.CheckEmailEdit(UserE.EMAIL, id))
            {
                if (roleModels.GetItem(UserE.ID_ROLE).NAME.Equals(ConstanAppkey.ADMIN()))
                {
                    if (!(id == prin.ID && prin.ROLE.Equals(ConstanAppkey.ADMIN())))
                    {
                        ViewBag.ListRole = roleModels.GetAllItems();
                        return View(UserE);
                    }
                }
                if (prin.ROLE.Equals(ConstanAppkey.ADMIN()) && id == prin.ID)
                {
                    UserE.ID_ROLE = roleModels.GetItemName(ConstanAppkey.ADMIN()).ID;
                }
                if (this.Request.Form["ACTIVE"] != null)
                {
                    UserE.ACTIVE = true;
                }
                else
                {
                    UserE.ACTIVE = false;
                }
                if (ModelState.IsValid)
                {
                    USER user1 = userModels.GetItem(id);
                    //upload file
                    string picture_new = "";
                    string picture_old = "";
                    if (Picture != null && Picture.ContentLength > 0)
                    {
                        // create new file
                        ImgUpload imgUpload = new ImgUpload();
                        var path = Server.MapPath("~/Assets/Upload/User");
                        UserE.PICTURE = imgUpload.Upload(Picture, path);
                        picture_new = UserE.PICTURE;
                        picture_old = user1.PICTURE;
                    }
                    else
                    {
                        UserE.PICTURE = user1.PICTURE;
                    }
                    if (userModels.EditItem(id, UserE) > 0)
                    {
                        if (!picture_old.Equals(""))
                        {
                            // delete file old
                            var dataFile = Server.MapPath("~/Assets/Upload/User/" + picture_old);
                            ImgUpload imgUpload = new ImgUpload();
                            imgUpload.Delete(dataFile);
                        }
                        Session["mes"] = "Sửa thành công";
                    }
                    else
                    {
                        if (!picture_new.Equals(""))
                        {
                            // delete file old
                            var dataFile = Server.MapPath("~/Assets/Upload/User/" + picture_new);
                            ImgUpload imgUpload = new ImgUpload();
                            imgUpload.Delete(dataFile);
                        }
                        Session["mes_er"] = "Sửa thất bại";
                    }
                    return RedirectToAction("Index", "UserManager");
                }
            }
            else
            {
                ViewBag.message_er = "Sửa thất bại! Email đã tồn tại";
            }
            ViewBag.ListRole = roleModels.GetAllItems();
            return View(UserE);
        }

        public ActionResult Delete(int id)
        {
            USER User = userModels.GetItem(id);
            if (Check.checkUserAdd(User))
            {
                string picture = User.PICTURE;
                int i = userModels.Delete(id);
                if (i > 0)
                {
                    var dataFile = Server.MapPath("~/Assets/Upload/User/" + picture);
                    ImgUpload imgUpload = new ImgUpload();
                    imgUpload.Delete(dataFile);
                    Session["mes"] = "Xóa thành công";
                }
                else if (i == -1)
                {
                    Session["mes_er"] = "Xóa bình luận của người này trước";
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
            return RedirectToAction("Index", "UserManager");
        }

    }
}