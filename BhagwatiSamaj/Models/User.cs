using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BhagwatiSamaj.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}