using Interconnected.Code;
using Interconnected.Code.CustomAuth;
using Interconnected.Models;
using Interconnected.Models.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Interconnected.Controllers
{
    public class LoginController : BaseController
    {
        UserModels userModels = new UserModels();
        RoleModels roleModels = new RoleModels();
        // GET: Login
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            if (Session["mes_su"] != null)
            {
                ViewBag.message_su = Session["mes_su"];
                Session.Remove("mes_su");
            }
            if (Session["mes_er"] != null)
            {
                ViewBag.message_er = Session["mes_er"];
                Session.Remove("mes_er");
            }
            ViewBag.Active = "login";
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(LOGIN Login)
        {
            if (!ModelState.IsValid)
            {
                return View(Login);
            }
            if (Membership.ValidateUser(Login.email, MyEndCode.mahoa(Login.password)))
            {
                var user = (CustomMemberShipUser)Membership.GetUser(Login.email, false);
                if (!user.ACTIVE)
                {
                    ViewBag.message = "Tài khoản chưa được kích hoạt. Vui lòng liên hệ admin để được kích hoạt";
                }
                else
                {
                    if (user != null)
                    {
                        CustomSerializeModel userModel = new CustomSerializeModel()
                        {
                            ID = user.ID,
                            EMAIL = user.EMAIL,
                            FULLNAME = user.FULLNAME,
                            ACTIVE = user.ACTIVE,
                            PICTURE = user.PICTURE,
                            PHONE = user.PHONE,
                            ADDRESS = user.ADDRESS,
                            ROLE = user.ROLE
                        };

                        string userData = JsonConvert.SerializeObject(userModel);
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                            (
                            1, Login.email, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData
                            );

                        string enTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie faCookie = new HttpCookie("Cookie1", enTicket);
                        Response.Cookies.Add(faCookie);
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.message = "Email hoặc mật khẩu không đúng";
            }
            return View(Login);
        }

        public ActionResult Regis()
        {
            if (Session["mes_er"] != null)
            {
                ViewBag.message_er = Session["mes_er"];
                Session.Remove("mes_er");
            }

            ViewBag.Active = "login";
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Regis(USER User, HttpPostedFileBase Picture)
        {
            if (ModelState.IsValid || User.PASSWORD != null || !User.PASSWORD.Equals(""))
            {
                if (userModels.CheckEmail(User.EMAIL))
                {
                    //upload file
                    if (Picture != null && Picture.ContentLength > 0)
                    {
                        var path = Server.MapPath("~/Assets/Upload/User/");
                        ImgUpload imgUpload = new ImgUpload();
                        User.PICTURE = imgUpload.Upload(Picture, path);
                    }
                    User.ACTIVE = false;
                    User.ID_ROLE = roleModels.GetItemName(ConstanAppkey.USER()).ID;
                    if (userModels.AddItem(User) > 0)
                    {
                        Session["mes_su"] = "Đăng ký thành công. chờ xét duyệt";
                        return RedirectToAction("Index", "Login");
                    }
                    else
                    {
                        Session["mes_er"] = "Đăng ký thất bại, thử lại!";
                        return RedirectToAction("Regis", "Login");
                    }
                }
                else
                {
                    Session["mes_er"] = "Email đã đăng ký!!!";
                }
            }
            return RedirectToAction("Regis", "Login");
        }

        public ActionResult Logout()
        {
            HttpCookie cookie = new HttpCookie("Cookie1", "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login", null);
        }
    }
}