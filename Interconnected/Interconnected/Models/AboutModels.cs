using Interconnected.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Interconnected.Models
{
    public class AboutModels
    {
        InterconnectedDb db;

        public AboutModels()
        {
            db = new InterconnectedDb();
        }

        public ABOUT GetItem()
        {
            return db.ABOUTS.FirstOrDefault();
        }

        public int Add(ABOUT About)
        {
            db.ABOUTS.Add(About);
            return db.SaveChanges();
        }

        internal int Edit(int id, ABOUT About)
        {
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    ABOUT AboutE = db.ABOUTS.Find(id);
                    AboutE.CONTENTS = About.CONTENTS;
                    db.Entry(AboutE).State = EntityState.Modified;
                    transaction.Commit();
                    return db.SaveChanges();
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