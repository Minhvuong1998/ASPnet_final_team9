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
    public class AboutController : BaseController
    {
        AboutModels aboutModels = new AboutModels();
        // GET: About
        public ActionResult Index()
        {
            ABOUT About = aboutModels.GetItem();
            ViewBag.Active = "about";
            return View(About);
        }

        [CustomAuthorize(Role = "admin")]
        public ActionResult Edit()
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

            ABOUT About = aboutModels.GetItem();
            ViewBag.Active = "manager";
            return View(About);
        }

        [CustomAuthorize(Role = "admin")]
        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Edit(ABOUT About, int id)
        {
            if (aboutModels.Edit(id, About) > 0)
            {
                Session["mes"] = "Sửa thành công";
            }
            else
            {
                Session["mes_er"] = "Sửa thất bại";
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}