using Interconnected.Code;
using Interconnected.Code.CustomAuth;
using Interconnected.Models;
using Interconnected.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Interconnected.Controllers
{
    [Authorize]
    public class ProfileManagerController : BaseController
    {
        UserModels userModels = new UserModels();
        RoleModels roleModels = new RoleModels();
        // GET: ProfileManager
        public ActionResult Index()
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
            USER UserProfile = userModels.GetItem(prin.ID);
            ViewBag.ListRole = roleModels.GetAllItems();
            return View(UserProfile);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(USER UserSubmit, FormCollection form, HttpPostedFileBase Picture)
        {
            CustomPrincipal prin = (CustomPrincipal)User;
            UserSubmit.ACTIVE = prin.ACTIVE;
            if (ModelState.IsValid)
            {
                if (userModels.CheckEmailEdit(UserSubmit.EMAIL, prin.ID))
                {
                    USER user1 = userModels.GetItem(prin.ID);
                    //upload file
                    string picture_new = "";
                    string picture_old = "";
                    if (Picture != null && Picture.ContentLength > 0)
                    {
                        // create new file
                        ImgUpload imgUpload = new ImgUpload();
                        var path = Server.MapPath("~/Assets/Upload/User/");
                        UserSubmit.PICTURE = imgUpload.Upload(Picture, path);
                        picture_new = UserSubmit.PICTURE;
                        picture_old = user1.PICTURE;
                    }
                    else
                    {
                        UserSubmit.PICTURE = user1.PICTURE;
                    }
                    UserSubmit.ID_ROLE = user1.ID_ROLE;
                    if (userModels.EditItem(prin.ID, UserSubmit) > 0)
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
                }
                else
                {
                    Session["mes_er"] = "Email đã tồn tại";
                }
            }
            return RedirectToAction("Index", "ProfileManager");
        }
    }
}