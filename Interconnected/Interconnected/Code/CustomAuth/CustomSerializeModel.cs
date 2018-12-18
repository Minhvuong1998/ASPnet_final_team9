using Interconnected.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Interconnected.Code.CustomAuth
{
    public class CustomSerializeModel
    {
        public int ID { get; set; }
        public string EMAIL { get; set; }
        public string FULLNAME { get; set; }
        public bool ACTIVE { get; set; }
        public string PICTURE { get; set; }
        public string PHONE { get; set; }
        public string ADDRESS { get; set; }

        public string ROLE;
    }
}