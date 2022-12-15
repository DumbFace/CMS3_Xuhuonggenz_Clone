using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webCMS3.Models
{
    public class WebName
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public enum Web {
              tuoitre,
              thanhnien,
              kenh14,
        }
    }
}