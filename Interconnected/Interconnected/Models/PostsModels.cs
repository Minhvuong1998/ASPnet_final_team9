using Interconnected.Code;
using Interconnected.Code.CustomAuth;
using Interconnected.Models.Entity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Interconnected.Models
{
    public class PostsModels
    {
        InterconnectedDb db;
        CommentsModels commentsModels;
        public PostsModels()
        {
            db = new InterconnectedDb();
            commentsModels = new CommentsModels();
        }

        public List<POST> GetItemActive(int page, int pageSize, int idPost)
        {
            return db.POSTs.Where(c => c.ACTIVE == true).Where(c => c.ID != idPost).OrderByDescending(c => c.ID).Take(5).ToList();
        }

        public List<POST> GetItemActiveSame(int page, int pageSize, int id, int idPost)
        {
            return db.POSTs.Where(c => c.ACTIVE == true).Where(c => c.ID_CATEGORY == id).Where(c => c.ID != idPost).OrderByDescending(c => c.ID).Take(5).ToList();
        }

        public IPagedList<POST> GetItems(int page, int pageSize)
        {
            return db.POSTs.OrderByDescending(c => c.ID).ToList().ToPagedList((page), pageSize);
        }

        internal IPagedList GetItemsMod(int page, int pageSize, CustomPrincipal prin)
        {
            string user = ConstanAppkey.USER();
            return db.POSTs.Where(c => (c.ID_USER == prin.ID) || (c.USER.ROLE.NAME.Equals(user))).OrderByDescending(c => c.ID).ToList().ToPagedList((page), pageSize);
        }

        public IPagedList<POST> GetItems(int page, int pageSize, CustomPrincipal prin)
        {
            return db.POSTs.Where(c=>c.ID_USER==prin.ID).OrderByDescending(c => c.ID).ToList().ToPagedList((page), pageSize);
        }

        public IPagedList GetItemsActiveCate(int page, int pageSize, int id)
        {
            return db.POSTs.Where(c => c.ID_CATEGORY == id).Where(c => c.ACTIVE).OrderByDescending(c => c.ID).ToList().ToPagedList((page), pageSize);
        }

        public POST GetItem(int id)
        {
            return db.POSTs.Find(id);
        }

        public IPagedList GetItemsSearch(string keySearch, int page, int pageSize)
        {
            return db.POSTs.Where(c => c.TITLE.Contains(keySearch)).OrderByDescending(c => c.ID).ToList().ToPagedList((page), pageSize);
        }

        public int AddItem(POST Post)
        {
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    Post.URL_SLUG = ConvertString.ToUrlSlug(Post.TITLE);
                    Post.DATE_CREATED = DateTime.Now;
                    db.POSTs.Add(Post);
                    int i = db.SaveChanges();
                    transaction.Commit();
                    return Post.ID;
                }
                catch
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }

        public int EditItem(int id, POST Post)
        {
            POST PostEdit = db.POSTs.Find(id);
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    PostEdit.TITLE = Post.TITLE;
                    PostEdit.URL_SLUG = ConvertString.ToUrlSlug(Post.TITLE);
                    PostEdit.DATE_EDIT = DateTime.Now;
                    PostEdit.ACTIVE = Post.ACTIVE;
                    PostEdit.DESCRIPTION = Post.DESCRIPTION;
                    PostEdit.DETAIL = Post.DETAIL;
                    PostEdit.PICTURE = Post.PICTURE;
                    db.Entry(PostEdit).State = EntityState.Modified;
                    int i = db.SaveChanges();
                    transaction.Commit();
                    return Post.ID;
                }
                catch
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }

        public int DeleteItem(int id)
        {
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    POST PostDelete = db.POSTs.Find(id);
                    commentsModels.DeleteMultiItemByIdPost(id);
                    db.POSTs.Remove(PostDelete);
                    int i = db.SaveChanges();
                    transaction.Commit();
                    return i;
                }
                catch
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }
    }
}