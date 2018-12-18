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
    public class CategoriesController : BaseController
    {
        CategoriesModels categoriesModels = new CategoriesModels();
        PostsModels postsModels = new PostsModels();
        CommentsModels commentsModels = new CommentsModels();
        VoteModels voteModels = new VoteModels();
        // GET: Categories
        public ActionResult Index(int id, int? page)
        {
            ViewBag.Category = categoriesModels.GetItem(id);
            ViewBag.Active = "category";
            IPagedList ListPost = postsModels.GetItemsActiveCate(page ?? 1, 12, id);
            return View(ListPost);
        }

        public ActionResult DetailPost(int id, int? page)
        {
            POST Post = postsModels.GetItem(id);
            ViewBag.Post = postsModels.GetItem(id);
            ViewBag.SamePost = postsModels.GetItemActiveSame(1, 5, Post.ID_CATEGORY, id);
            ViewBag.RencentPost = postsModels.GetItemActive(1, 5, id);
            ViewBag.Active = "category";
            ViewBag.Vote = 0;
            if (User.GetType() == typeof(CustomPrincipal))
            {
                CustomPrincipal prin = (CustomPrincipal)User;
                VOTE Vote = voteModels.GetItemPostUser(Post.ID, prin.ID);
                if (Vote != null)
                {
                    ViewBag.Vote = 1;
                }
                else
                {
                    ViewBag.Vote = 2;
                }
            }
            IPagedList ListCommentPost = commentsModels.GetItemsPost(page ?? 1, 12, id);
            return View(ListCommentPost);
        }

        [Authorize]
        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult PostComment(int idPost, FormCollection form)
        {
            POST Post=postsModels.GetItem(idPost);
            if (!form["DETAIL"].Equals(""))
            {
                COMMENT Cm = new COMMENT();
                CustomPrincipal prin = (CustomPrincipal)User;
                Cm.DETAIL = form["DETAIL"];
                Cm.ID_USER = prin.ID;
                Cm.ID_POST = idPost;
                Cm.DATE_CREATED = DateTime.Now;
                int i = commentsModels.Add(Cm);
            }
            return RedirectToAction("DetailPost", "Categories", new { id = idPost, post = Post.URL_SLUG, idCate = Post.ID_CATEGORY, category = Post.CATEGORy.URL_SLUG});
        }

        [Authorize]
        public ActionResult VotePost(int idPost)
        {
            if (User.GetType() == typeof(CustomPrincipal))
            {
                CustomPrincipal prin = (CustomPrincipal)User;
                VOTE Vote = voteModels.GetItemPostUser(idPost, prin.ID);
                if (Vote == null)
                {
                    VOTE VoteAdd = new VOTE();
                    VoteAdd.ID_POST = idPost;
                    VoteAdd.ID_USER = prin.ID;
                    int i = voteModels.Add(VoteAdd);
                }
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}