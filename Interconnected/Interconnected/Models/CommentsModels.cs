using Interconnected.Models.Entity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Interconnected.Models
{
    public class CommentsModels
    {
        InterconnectedDb db;
        public CommentsModels()
        {
            db = new InterconnectedDb();
        }

        internal IPagedList GetItemsPost(int page, int pageSize, int id)
        {
            return db.COMMENTS.Where(c => c.ID_POST == id).ToList().ToPagedList((page), pageSize);
        }

        public void DeleteMultiItemByIdPost(int id)
        {
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.COMMENTS.RemoveRange(db.COMMENTS.Where(c => c.ID_POST == id));
                    int i = db.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }

        public int Add(COMMENT Cm)
        {
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.COMMENTS.Add(Cm);
                    int i = db.SaveChanges();
                    transaction.Commit();
                    return Cm.ID;
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