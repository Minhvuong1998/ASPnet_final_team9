using Interconnected.Code;
using Interconnected.Models.Entity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Interconnected.Models
{
    public class UserModels
    {
        InterconnectedDb db;
        int ID_ROLE;
        public UserModels()
        {
            db = new InterconnectedDb();
            ID_ROLE = ConstanAppkey.ID_ROLE_SUPER();
        }

        public IPagedList GetItems(int page, int pageSize)
        {
            return db.USERS.OrderByDescending(c => c.ID).ToList().ToPagedList((page), pageSize);
        }

        internal IPagedList GetItemsMod(int page, int pageSize, Code.CustomAuth.CustomPrincipal prin)
        {
            string user = ConstanAppkey.USER();
            return db.USERS.Where(c => c.ROLE.NAME.Equals(user)||c.ID==prin.ID).OrderByDescending(c => c.ID).ToList().ToPagedList((page), pageSize);
        }

        public USER GetItem(int id)
        {
            return db.USERS.Find(id);
        }

        public int AddItem(USER User)
        {
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    User.PASSWORD = MyEndCode.mahoa(User.PASSWORD);
                    db.USERS.Add(User);
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

        public int EditItem(int id, USER User)
        {
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    USER U = db.USERS.Find(id);
                    if (User.PASSWORD != null && !User.PASSWORD.Equals(""))
                    {
                        U.PASSWORD = MyEndCode.mahoa(User.PASSWORD);
                    }
                    U.FULLNAME = User.FULLNAME;
                    U.ACTIVE = User.ACTIVE;
                    U.PICTURE = User.PICTURE;
                    U.PHONE = User.PHONE;
                    U.ADDRESS = User.ADDRESS;
                    U.ID_ROLE = User.ID_ROLE;
                    db.Entry(U).State = EntityState.Modified;
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

        public int Delete(int id)
        {
            USER user = db.USERS.Find(id);
            COMMENT cm = db.COMMENTS.Where(c => c.ID_USER == id).FirstOrDefault();
            if (cm != null)
            {
                return -1;
            }
            POST Post = db.POSTs.Where(c => c.ID_USER == id).FirstOrDefault();
            if (Post != null)
                return -1;
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.USERS.Remove(user);
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

        internal bool CheckEmail(string email)
        {
            USER UseCheck = db.USERS.Where(c => c.EMAIL.Equals(email)).FirstOrDefault();
            if (UseCheck == null)
            {
                return true;
            }
            return false;
        }

        internal bool CheckEmailEdit(string email, int id)
        {
            USER UserEdit = db.USERS.Find(id);
            USER User = db.USERS.Where(c => c.EMAIL.Equals(email)).Where(c => c.ID!=id).FirstOrDefault();
            if (User == null)
                return true;
            return false;
        }
    }
}