using Interconnected.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Interconnected.Models
{
    public class RoleModels
    {
        InterconnectedDb db;
        public RoleModels()
        {
            db = new InterconnectedDb();
        }

        public List<ROLE> GetAllItems()
        {
            return db.ROLES.OrderByDescending(c => c.ID).ToList();
        }

        public ROLE GetItemName(string Name)
        {
            return db.ROLES.Where(c => c.NAME.Equals(Name)).FirstOrDefault();
        }

        internal ROLE GetItem(int idRole)
        {
            return db.ROLES.Find(idRole);
        }
    }
}