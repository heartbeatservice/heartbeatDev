using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HBS.Data.Abstract;
using HBS.Data.Concrete;
using HBS.Entities;
namespace HBS.WebApi.Controllers
{
    public class UserController : ApiController
    {
        public ISecurityRepository securityEntity;

        public UserController(ISecurityRepository repo)
        {
            this.securityEntity = repo;
            

         }

        public UserProfile GetUser(string id)
        {
            return securityEntity.GetUser(Convert.ToInt32(id));
        }

        public UserProfile PostUser([FromBody] UserProfile user)
        {
            return securityEntity.GetUser(securityEntity.AddUser(user));
        }
    }
}
