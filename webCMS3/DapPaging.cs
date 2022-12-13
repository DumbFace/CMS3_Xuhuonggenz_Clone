using Dapper;
using MvcPaging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webCMS3.Models;

namespace webCMS3
{
    public static class DapPaing
    {
        private const string SqlPagging =
                        @"WITH ListPaged As
            ( SELECT    ROW_NUMBER() OVER ( ORDER BY {0}) AS RowNum, {1}
                      FROM      {2}
                      WHERE     {3}
            )
            SELECT * FROM ListPaged
            WHERE   RowNum > {4}
                AND RowNum < {5}";

        public static IPagedList<T> QueryPaging<T>(this MySqlConnection conn, string sqlSelect, string sqlFrom, 
            string sqlWhere, string orderBy, dynamic param = null, int pageindex = 1, int? pagesize = 10)
        {
            string querypaging;
            int totalRecord;
            int i = pageindex;
            pagesize = pagesize ?? 20;

            string temp = String.Format("SELECT COUNT(1) FROM {0} WHERE {1}", sqlFrom, sqlWhere);

            totalRecord = conn.Query<int>(String.Format("SELECT COUNT(1) FROM {0} WHERE {1}", sqlFrom, sqlWhere), (object)param).First();
            var from = pageindex * pagesize.Value;
            var to = (from + pagesize.Value + 1) <= 1 ? int.MaxValue : from + pagesize.Value + 1;

            querypaging = String.Format(SqlPagging, orderBy, sqlSelect, sqlFrom, sqlWhere, from, to);

            return conn.Query<T>(querypaging, (object)param).ToPagedList(pageindex, (pagesize > 0 ? pagesize.Value : int.MaxValue), totalRecord);
        }

        public static T Cast<T>(object o)
        {
            return (T)o;
        }
    }

}