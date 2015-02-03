using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HBS.Data.Abstract;
using HBS.Data.Concrete;
using HBS.Entities;
using System.Web;
namespace HBS.WebApi.Controllers
{
    public class SecurityController : ApiController
    {
        public ISecurityRepository securityEntity;

        public SecurityController(ISecurityRepository repo)
        {
            this.securityEntity = repo;
            

         }

        public UserProfile GetUserByName(string id)
        {
            return securityEntity.GetUser(id);
        }

        [HttpPost]
        public UserProfile PostUser([FromBody] UserProfile user)
        
        {
            
            
            UserProfile userInRepo;
            userInRepo = securityEntity.GetUser(user.UserName);
            if (userInRepo == null)
                userInRepo = new UserProfile() { UserId = -2 };
            else if (!(user.Password == userInRepo.Password))
                userInRepo.UserId = -1;
            return userInRepo;
        }

        [AcceptVerbs("OPTIONS")]
        public HttpResponseMessage Options()
        {
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
        
            return resp;
        }
        }
}
