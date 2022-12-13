using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webCMS3.Models
{
    public class tblNews
    {
        public int Id { get; set; }
        public int IdZone { get; set; }
        public string ZoneName { get; set; }
        public int IdCategory { get; set; }
        public string CategoryName { get; set; }
        public string Title { get; set; }
        public string Teaser { get; set; }
        public string Avatar { get; set; }
        public int IdUser { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime PublishDate { get; set; }
        public string Body { get; set; }
        public int Status { get; set; }
        public string UrlLink { get; set; }
        public int HomeFeature { get; set; }
        public int CatFeature { get; set; }
        public int ViewCount { get; set; }
        public string AvatarThumb { get; set; }
        public string AvatarFB { get; set; }
        public List<tblTag> Tags { get; set; }
        public bool IsFeature { get; set; }
    }
}