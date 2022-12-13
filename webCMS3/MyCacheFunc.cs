using MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webCMS3.Models;

namespace webCMS3
{
    public class MyCacheFunc
    {
        private static RedisCache redis = new RedisCache();

        public static tblNews GetNews(int Id)
        {
            string strKey = myCommon.RedisSuffix + string.Format(myCommon.hsContentId, Id);
            tblNews tbl = redis.Get<tblNews>(strKey);
            if (tbl == null)
            {
                tbl = new myfunc().GetContentNewsbyId(Id);
                redis.Set(strKey, tbl);
            }
            return tbl;
        }

        public static void SetNews(int Id)
        {
            string strKey = myCommon.RedisSuffix + string.Format(myCommon.hsContentId, Id);
            redis.Remove(strKey);
            redis.Set(strKey, new myfunc().GetContentNewsbyId(Id));
        }

        public static void RemoveNews(int Id)
        {
            string strKey = myCommon.RedisSuffix + string.Format(myCommon.hsContentId, Id);
            redis.Remove(strKey);
        }

        public static void SetLastNewsHome()
        {
            string strKey = myCommon.RedisSuffix + myCommon.hsLastHome;
            redis.Remove(strKey);
            redis.Set(strKey, new myfunc().GetLastNewsHome(8));
        }

        public static List<tblHome> GetLastNews()
        {
            string strKey = myCommon.RedisSuffix + myCommon.hsLastHome;
            List<tblHome> lst = redis.Get<List<tblHome>>(strKey);
            if (lst == null || lst.Count == 0)
            {
                lst = new myfunc().GetLastNewsHome(8);
                redis.Set(strKey, lst);
            }
            return lst;
        }

        public static void SetTags()
        {
            string strKey = myCommon.RedisSuffix + myCommon.hsTags;
            redis.Remove(strKey);
            redis.Set(strKey, new myfunc().GetListTagView(10));
        }
        public static List<tblTag> GetTags()
        {
            string strKey = myCommon.RedisSuffix + myCommon.hsTags;
            List<tblTag> lst = redis.Get<List<tblTag>>(strKey);
            if (lst == null || lst.Count == 0)
            {
                lst = new myfunc().GetListTagView(10);
                redis.Set(strKey, lst);
            }
            return lst;
        }

        public static void SetNewsHome()
        {
            string strKey = myCommon.RedisSuffix + myCommon.hsNewsHome;
            redis.Remove(strKey);
            redis.Set(strKey, new myfunc().GetAllNewsHome());
        }

        public static List<tblHome> GetNewsHome()
        {
            string strKey = myCommon.RedisSuffix + string.Format(myCommon.hsNewsHome);
            List<tblHome> lst = redis.Get<List<tblHome>>(strKey);
            if (lst == null || lst.Count == 0)
            {
                lst = new myfunc().GetAllNewsHome();
                redis.Set(strKey, lst);
            }
            return lst;
        }
        public static void SetTag(int Id)
        {
            string strKey = myCommon.RedisSuffix + string.Format(myCommon.hsTagId, Id);
            redis.Remove(strKey);
            redis.Set(strKey, new myfunc().GetTagbyId(Id));
        }

        public static tblTag GetTag(int Id)
        {
            string strKey = myCommon.RedisSuffix + string.Format(myCommon.hsTagId, Id);
            tblTag tbl = redis.Get<tblTag>(strKey);
            if (tbl == null)
            {
                tbl = new myfunc().GetTagbyId(Id);
                redis.Set(strKey, tbl);
            }
            return tbl;
        }

        public static void SetTopTagNews(int Id)
        {
            string strKey = myCommon.RedisSuffix + string.Format(myCommon.hsTopTagNews, Id);
            redis.Remove(strKey);
            redis.Set(strKey, new myfunc().GetTopTag(Id));
        }

        public static tblNews GetTopTagNews(int Id)
        {
            string strKey = myCommon.RedisSuffix + string.Format(myCommon.hsTopTagNews, Id);
            tblNews tbl = redis.Get<tblNews>(strKey);
            if (tbl == null)
            {
                tbl = new myfunc().GetTopTag(Id);
                redis.Set(strKey, tbl);
            }
            return tbl;
        }

        public static List<tblHome> GetNewsByIdTag(int Id)
        {
            string strKey = myCommon.RedisSuffix + string.Format(myCommon.hsNewsByTag, Id);
            List<tblHome> lst = redis.Get<List<tblHome>>(strKey);
            if (lst == null || lst.Count == 0)
            {
                lst = new myfunc().GetAllNewsByIdTag(Id);
                redis.Set(strKey, lst);
            }
            return lst;
        }

        public static void SetNewsByIdTag(int Id)
        {
            string strKey = myCommon.RedisSuffix + string.Format(myCommon.hsNewsByTag, Id);
            redis.Remove(strKey);
            redis.Set(strKey, new myfunc().GetAllNewsByIdTag(Id));
        }

        public static void RemoveKeyNewsTag(int Id)
        {
            string strKey = myCommon.RedisSuffix + string.Format(myCommon.hsNewsByTag, Id);
            redis.Remove(strKey);
        }

        public static void RemoveKeyTopTagNews(int Id)
        {
            string strKey = myCommon.RedisSuffix + string.Format(myCommon.hsTopTagNews, Id);
            redis.Remove(strKey);
        }

        public static void SetZones()
        {
            string strKey = myCommon.RedisSuffix + myCommon.hsZones;
            redis.Remove(strKey);
            redis.Set(strKey, new myfunc().GetZonesHeader(10));
        }

        public static List<tblZone> GetZones()
        {
            string strKey = myCommon.RedisSuffix + myCommon.hsZones;
            List<tblZone> lst = redis.Get<List<tblZone>>(strKey);
            if (lst == null || lst.Count == 0)
            {
                lst = new myfunc().GetZonesHeader(10);
                redis.Set(strKey, lst);
            }
            return lst;
        }

        public static void SetZone(int Id)
        {
            string strKey = myCommon.RedisSuffix + string.Format(myCommon.hsZoneId, Id);
            redis.Remove(strKey);
            redis.Set(strKey, new myfunc().GetZonebyId(Id));
        }

        public static tblZone GetZone(int Id)
        {
            string strKey = myCommon.RedisSuffix + string.Format(myCommon.hsZoneId, Id);
            tblZone lst = redis.Get<tblZone>(strKey);
            if (lst == null)
            {
                lst = new myfunc().GetZonebyId(Id);
                redis.Set(strKey, lst);
            }
            return lst;
        }

        public static void SetNewsByIdZone(int Id)
        {
            string strKey = myCommon.RedisSuffix + string.Format(myCommon.hsNewsByZone, Id);
            redis.Remove(strKey);
            redis.Set(strKey, new myfunc().GetAllNewsByIdZone(Id));
        }

        public static List<tblHome> GetNewsByIdZone(int Id)
        {
            string strKey = myCommon.RedisSuffix + string.Format(myCommon.hsNewsByZone, Id);
            List<tblHome> lst = redis.Get<List<tblHome>>(strKey);
            if (lst == null || lst.Count == 0)
            {
                lst = new myfunc().GetAllNewsByIdZone(Id);
                redis.Set(strKey, lst);
            }
            return lst;
        }

        public static void SetArticlesFeature()
        {
            string strKey = myCommon.RedisSuffix + myCommon.hsNewsFeature;
            redis.Remove(strKey);
            redis.Set(strKey, new myfunc().GetArticlesFeature());
        }

        public static List<tblNews> GetArticlesFeature()
        {
            string strKey = myCommon.RedisSuffix + myCommon.hsNewsFeature;
            List<tblNews> lst = redis.Get<List<tblNews>>(strKey);
            if (lst == null || lst.Count == 0)
            {
                lst = new myfunc().GetArticlesFeature();
                redis.Set(strKey, lst);
            }
            return lst;
        }
        public static void RemoveKeyZoneNews(int Id)
        {
            string strKey = myCommon.RedisSuffix + string.Format(myCommon.hsNewsByZone, Id);
            redis.Remove(strKey);
        }

        public static void SetConfigWeb()
        {
            string strKey = myCommon.RedisSuffix + myCommon.hsConfigWeb;
            redis.Remove(strKey);
            redis.Set(strKey, new myfunc().GetConfigWeb());
        }

        public static tblConfig GetConfigWeb()
        {
            string strKey = myCommon.RedisSuffix + myCommon.hsConfigWeb;
            tblConfig lst = redis.Get<tblConfig>(strKey);
            if (lst == null)
            {
                lst = new myfunc().GetConfigWeb();
                redis.Set(strKey, lst);
            }
            return lst;
        }
    }
}