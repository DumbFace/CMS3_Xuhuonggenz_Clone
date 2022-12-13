using StackExchange.Redis;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using webCMS3.Models;
using MvcPaging;

namespace webCMS3
{
    public class RedisCache
    {
        private readonly IDatabase _db;

        public RedisCache()
        {
            this._db = RedisConnectorHelper.GetDatabase();
        }

        public virtual T Get<T>(string key)
        {
            var rValue = _db.StringGet(key);

            if (!rValue.HasValue)
                return default(T);
            else
                return Deserialize<T>(rValue);
        }

        protected virtual byte[] Serialize(object item)
        {
            var jsonString = JsonConvert.SerializeObject(item);
            return Encoding.UTF8.GetBytes(jsonString);
        }
        protected virtual T Deserialize<T>(byte[] serializedObject)
        {
            try
            {
                if (serializedObject == null)
                    return default(T);

                var jsonString = Encoding.UTF8.GetString(serializedObject);
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            catch
            {
                return default(T);
            }
        }

        public virtual void Set(string key, object data, int cacheTime = 8640000)
        {
            if (data == null)
                return;

            var entryBytes = Serialize(data);
            var expiresIn = TimeSpan.FromMinutes(cacheTime);
            _db.StringSet(key, entryBytes, expiresIn);
        }

        public virtual bool IsSet(string key)
        {
            return _db.KeyExists(key);
        }

        public virtual void Remove(string key)
        {
            _db.KeyDelete(key);
        }
    }
}