using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using HBS.Entities;
using System.Text;

namespace HBS.WebPortal.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int userid { get; set; }
        public int companyid { get; set; }

        public bool ValidatePassword()
        {
            HBS.Entities.UserProfile u=new HBS.Entities.UserProfile();
            u.UserName=this.UserName;
            u.Password=this.Password;
            string reqText=JsonConvert.SerializeObject(u);
            byte[] data = Encoding.UTF8.GetBytes(reqText);
            bool result = false;

            string uri = "http://services.heartbeat-biz.com/api/Security";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);

                   req.Method = "post";
                   req.ContentType = "application/json";
                   req.Accept = "text/json";
                   Stream requestStream = req.GetRequestStream();
                   requestStream.Write(data, 0, data.Length);
                   requestStream.Close();
                   HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                   //Now Reading the Response to the end. This is basically a JSON Object comprised of the JSON Array Mentioned in comment above
                   StreamReader read = new StreamReader(response.GetResponseStream());
                   string ResponseJSon = read.ReadToEnd();
                   read.Close();
                   u = JsonConvert.DeserializeObject<HBS.Entities.UserProfile>(ResponseJSon);
                   this.userid = u.UserId;
                   this.companyid = u.CompanyId;
                   
            return result;
        }
    }
}