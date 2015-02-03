using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using ServiceStack.Redis;
using System.Text;
namespace HBS.WebPortal.Models
{
    public class RapidRedis<T>
    {
        static string keyList="KeyList";
        IRedisNativeClient client;

        public RapidRedis()
        {
            client = new RedisClient("162.243.79.25", 6379, "Karachi@8681", 0);

        }
        public void InsertObject(T obj,string key)
        {
            //using (IRedisNativeClient client = new RedisClient("162.243.79.25",6379,"Karachi@8681",0))
            //{
            //    var test=Encoding.UTF8.GetString(client.Get("test"));
            //    ViewBag.Test = test;

            //}
            Byte[] blob = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));
            
                client.Set(key, blob);

               
            
        }

        public void AddKeyToList(string key)
        {
            
            Byte[] blob=Encoding.UTF8.GetBytes(key);
            client.LPush(keyList, blob);

        }

        public long GetKeyListLenght()
        {
            return client.LLen(keyList);
        }

        public List<string> GetList()
        {
            List<string> myList = new List<string>();
            long len=GetKeyListLenght()-1;
            Byte[][] listData = client.LRange(keyList, 0, (int)len);
            Byte[] data;
            for(int i=0;i<listData.Length;i++)
            {
                data = listData[i];
                myList.Add(Encoding.UTF8.GetString(data));
            }

            return myList;
        }

    }
}