using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace webCMS3
{
    public class RssFeed : FileResult
    {
        private Uri _currentUrl;
        private readonly string _title;
        private readonly string _description;
        private readonly List<SyndicationItem> _items;

        public RssFeed(string contentType, string title, string description, List<SyndicationItem> items, string currentUrl)
            : base(contentType)
        {
            _title = title;
            _description = description;
            _items = items;
            _currentUrl = new Uri(currentUrl);
        }

        protected override void WriteFile(HttpResponseBase response)
        {
            var feed = new SyndicationFeed(title: this._title, description: _description, feedAlternateLink: _currentUrl,
                                           items: this._items);
            var formatter = new Rss20FeedFormatter(feed);
            using (var writer = XmlWriter.Create(response.Output))
            {
                formatter.WriteTo(writer);
            }
        }

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);
        }
    }
}