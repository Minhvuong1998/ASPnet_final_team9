using Interconnected.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Interconnected.Models
{
    public class VoteModels
    {
        InterconnectedDb db;

        public VoteModels()
        {
            db = new InterconnectedDb();
        }

        public VOTE GetItemPostUser(int idPost, int idUser)
        {
            return db.VOTEs.Where(c => c.ID_POST == idPost).Where(c => c.ID_USER == idUser).FirstOrDefault();
        }

        public int Add(VOTE Vote)
        {
            db.VOTEs.Add(Vote);
            return db.SaveChanges();
        }
    }
}