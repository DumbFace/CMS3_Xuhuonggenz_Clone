using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webCMS3.Models
{
    public class tblUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int IdRole { get; set; }
        public int IsDelete { get; set; }
    }
}