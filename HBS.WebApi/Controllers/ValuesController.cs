using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HBS.Data.Abstract;
using HBS.Entities;

namespace HBS.WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        private ICommonRepository _commonRepository;

        public ValuesController(ICommonRepository commonRepository)
        {
            _commonRepository = commonRepository;
        }
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            var company = _commonRepository.GetCompnay(id);
            if (company != null)
                return company.CompanyName;

            return "Company Not Found";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}