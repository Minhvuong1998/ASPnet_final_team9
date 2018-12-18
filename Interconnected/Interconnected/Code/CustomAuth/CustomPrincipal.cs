using Interconnected.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Interconnected.Code.CustomAuth
{
    public class CustomPrincipal : IPrincipal
    {
        public int ID { get; set; }
        public string EMAIL { get; set; }
        public string FULLNAME { get; set; }
        public bool ACTIVE { get; set; }
        public string PICTURE { get; set; }
        public string PHONE { get; set; }
        public string ADDRESS { get; set; }

        public string ROLE;
        public IIdentity Identity
        {
            get;
            private set;
        }

        public bool IsInRole(string role)
        {
            if (ROLE.Equals(role))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public CustomPrincipal(string email)
        {
            Identity = new GenericIdentity(email);
        } 
    }
}