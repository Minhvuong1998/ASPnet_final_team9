using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interconnected.Models;
using Interconnected.Models.Entity;
using System.Data.Entity;
using Interconnected.Code;
using PagedList;

namespace Interconnected.Models
{
    public class CategoriesModels
    {
        InterconnectedDb db;

        public CategoriesModels()
        {
            db = new InterconnectedDb();
        }

        public List<CATEGORy> GetAllItem()
        {
            return db.CATEGORIES.OrderByDescending(c => c.ID).ToList();
        }

        internal CATEGORy GetItem(int id)
        {
            return db.CATEGORIES.Find(id);
        }

        public IPagedList GetItems(int page, int pageSize)
        {
            return db.CATEGORIES.OrderByDescending(c => c.ID).ToList().ToPagedList((page), pageSize);
        }

        public int AddItem(CATEGORy Category)
        {
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    Category.URL_SLUG = ConvertString.ToUrlSlug(Category.NAME);
                    db.CATEGORIES.Add(Category);
                    int i = db.SaveChanges();
                    transaction.Commit();
                    return Category.ID;
                }
                catch
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }

        public int EditItem(int id, CATEGORy Category)
        {
            CATEGORy CategoryEdit = db.CATEGORIES.Find(id);
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    CategoryEdit.NAME = Category.NAME;
                    CategoryEdit.URL_SLUG = ConvertString.ToUrlSlug(Category.NAME);
                    CategoryEdit.DESCRIPTION = Category.DESCRIPTION;
                    CategoryEdit.ID_PARENT = Category.ID_PARENT;
                    db.Entry(CategoryEdit).State = EntityState.Modified;
                    int i = db.SaveChanges();
                    transaction.Commit();
                    return CategoryEdit.ID;
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
            POST Post = db.POSTs.Where(c => c.ID_CATEGORY == id).FirstOrDefault();
            if (Post != null)
            {
                return -1;
            }
            CATEGORy Category = db.CATEGORIES.Where(c => c.ID_PARENT == id).FirstOrDefault();
            if (Category != null)
            {
                return -1;
            }
            db.CATEGORIES.Remove(db.CATEGORIES.Find(id));
            return db.SaveChanges();
        }
    }
}