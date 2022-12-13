using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webCMS3.Models
{
    public class tblHome
    {
        public int Id { get; set; }
        public int IdZone { get; set; }
        public string Title { get; set; }
        public string UrlLink { get; set; }
        public string CategoryName { get; set; }
        public string UserName { get; set; }
        public string Teaser { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Avatar { get; set; }
        public string AvatarThumb { get; set; }
        public string Body { get; set; }
    }
}