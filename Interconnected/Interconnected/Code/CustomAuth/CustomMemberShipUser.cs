using Interconnected.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Interconnected.Code.CustomAuth
{
    public class CustomMemberShipUser : MembershipUser
    {
        public int ID { get; set; }
        public string EMAIL { get; set; }
        public string FULLNAME { get; set; }
        public bool ACTIVE { get; set; }
        public string PICTURE { get; set; }
        public string PHONE { get; set; }
        public string ADDRESS { get; set; }

        public string ROLE;

        public CustomMemberShipUser(USER user)
            : base("CustomMembership", user.EMAIL, user.ID, "", string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            ID = user.ID;
            EMAIL = user.EMAIL;
            FULLNAME = user.FULLNAME;
            ACTIVE = user.ACTIVE;
            PICTURE = user.PICTURE;
            ROLE = user.ROLE.NAME;
        }  
    }
}